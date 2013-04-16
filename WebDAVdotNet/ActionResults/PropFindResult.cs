using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using System.Web;
using System.Web.Mvc;

using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

using WebDAVdotNet;
using WebDAVdotNet.Methods;

namespace WebDAVdotNet.ActionResults
{
    public class PropFindResult : ActionResult
    {
        static XmlSchemaSet _schemas;

        private WebDAVEngineBase _engine;
        private Headers.Depth _depth = Headers.Depth.Inifinity; // default per [RFC4918] p29

        static PropFindResult()
        {
            ValidationEventHandler validator = delegate(object sender, ValidationEventArgs e) { };
            _schemas = new XmlSchemaSet();
            XmlSchema schema = XmlSchema.Read(Assembly.GetExecutingAssembly().GetManifestResourceStream("WebDAVdotNet.Schemas.WebDAV.xsd"), validator);
            _schemas.Add(schema);
        }

        public PropFindResult(WebDAVEngineBase engine)
        {
            _engine = engine;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ValidationEventHandler validator = delegate(object sender, ValidationEventArgs e)
            {
                // TODO : validating the schema.  Does this matter?
            };

            XNamespace ns = "DAV:";
            string input = new StreamReader(context.HttpContext.Request.InputStream).ReadToEnd();
            XDocument doc = XDocument.Parse(input);
            doc.Validate(_schemas, validator);

            int depth = -1;
            if (context.HttpContext.Request.Headers["Depth"] == null)
            {
                _depth = Headers.Depth.One; //If not specified, RFC4918 says anticipate "Infinity".  
                // Also says Infinity may be disallowed for performance, so covert to 1
            }
            else if (Int32.TryParse(context.HttpContext.Request.Headers["Depth"], out depth))
            {
                if (depth == 0)
                    _depth = Headers.Depth.Zero;
                else if (depth == 1)
                    _depth = Headers.Depth.One;
                else
                    throw new ArgumentOutOfRangeException("Header DEPTH has an invalid value.  Expected values are \"0\", \"1\", or \"Infinity\".");
            }
            else
            {
                _depth = Headers.Depth.One; //probably said infinity, but we do 1 for performance
            }

            string _path = context.HttpContext.Request.Path;
            XElement xlPropFind = doc.Elements(ns + "propfind").FirstOrDefault();
            if (xlPropFind != null)
            {
                XElement node = null;

                // Return specific properties
                if ((node = xlPropFind.Element(ns + "prop")) != null)
                {
                    // A list of requested properties
                    List<PropertyName> properties = (from p in node.Descendants()
                                                     select new PropertyName(p.Name.NamespaceName, p.Name.LocalName)).ToList();
                    IEnumerable<IHierarchyItem> items = _engine.GetResources(new List<string>() { _path }, properties, _depth);

                    FillOutProperties(items, properties);

                    XNamespace davNS = XNamespace.Get("DAV:");
                    XElement multistatus = new XElement(davNS + "multistatus", new XAttribute(XNamespace.Xmlns + "D", davNS));

                    // Each resource is a different "DAV:response" line
                    foreach (IHierarchyItem item in items)
                    {
                        XElement response = new XElement(davNS + "response"
                            , new XElement(davNS + "href", item.Path));

                        // each status is unique within the response/resource
                        foreach (PropertyValue.Status status in Enums.Get<PropertyValue.Status>())
                        {
                            if (item.Properties.Count(r => r.Value.PropertyStatus == status) > 0)
                            {
                                XElement propstat = new XElement(davNS + "propstat",
                                                    from p in item.Properties.Where(p => p.Value.PropertyStatus == status)
                                                    join pr in properties on p.Key equals pr
                                                    select new XElement(davNS + "prop",
                                                                new XElement(XNamespace.Get(p.Key.Namespace) + p.Key.Name,
                                                                       p.Value.Value)   //property
                                                                        )   //end prop
                                                                , new XElement(davNS + "status", StatusCodes.GetHttpCode((int)status))
                                                                );  //end propstat

                                response.Add(propstat);
                            }
                        }

                        multistatus.Add(response);
                    }


                    context.HttpContext.Response.StatusCode = 207;
                    context.HttpContext.Response.AppendHeader("Content-Type", "application/xml");
                    using (StreamWriter sw = new StreamWriter(context.HttpContext.Response.OutputStream))
                    {
                        sw.Write(multistatus.ToString());
                    }

                }

                // Retrieve all property names
                else if ((node = xlPropFind.Element(ns + "propname")) != null)
                {

                }
                // Retrieve "allprop"
                else if ((node = xlPropFind.Element(ns + "allprop")) != null)
                {
                    // search for the "include"
                }

            }
        }


        /// <summary>
        /// Ensures that all specified properties are listed for the specified hierarchy items
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="items"></param>
        private static void FillOutProperties(IEnumerable<IHierarchyItem> items, List<PropertyName> properties)
        {
            foreach (IHierarchyItem item in items)
            {
                foreach (PropertyName name in properties)
                {
                    if (!item.Properties.ContainsKey(name))
                    {
                        item.Properties.Add(name, new PropertyValue(name));
                    }
                }
            }
        }
    }


}
