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
    public class GetResult : ActionResult
    {

        private WebDAVEngineBase _engine;

        public GetResult(WebDAVEngineBase engine)
        {
            _engine = engine;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            string path = context.RequestContext.HttpContext.Request.Path;
            IEnumerable<IHierarchyItem> items = _engine.GetResources(new List<string>() { path }, null, Headers.Depth.Zero);

            if (items.Count() > 1)
                throw new Exception("too many results - should be 0 or 1");

            IHierarchyItem item = items.First();
            HttpResponseBase response = context.RequestContext.HttpContext.Response;

            if (item != null && item is IFile)
            {

                IFile file = item as IFile;

                if (file.Properties.ContainsKey(PropertyName.CONTENTTYPE))
                    response.ContentType = file.Properties[PropertyName.CONTENTTYPE].Value.ToString();
                response.AddHeader("Content-Disposition", file.Name);
                response.BufferOutput = false;

                _engine.WriteFileToStream(file, response.OutputStream);
            }
            // else {}  // we should provide a handler to output the contents - or offer up a default one

            response.End();

        }
    }
}
