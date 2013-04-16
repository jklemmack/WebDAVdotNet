//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace WebDAVdotNet.Methods
//{
//    public class PropFind
//    {
//        private WebDAVEngineBase _engine;

//        public PropFind(WebDAVEngineBase engine)
//        {
//            _engine = engine;
//        }

//        /// <summary>
//        /// Retrieves named properties
//        /// </summary>
//        /// <param name="resource">A resource, either an object or a collection</param>
//        /// <param name="depth">Depth specifier</param>
//        /// <param name="properties">The list of named properties to retrieve</param>
//        /// <returns>A collection of all resouces matched by the resource query and depth, and the value and status of each requested property.</returns>
//        public static ICollection<IHierarchyItem> Prop(WebDAVEngineBase engine, string resource, WebDAVdotNet.Headers.Depth depth, IList<Property> properties)
//        {


//            engine.GetResources(new List<string> { resource }, properties, depth);
//            //string[] resourceHrefs = engine.GetResources(resource, depth);
//            //Resource[] resources = resourceHrefs.Select(h => new Resource(h, properties.Select(p => new PropertyStatus(p)).ToArray())).ToArray();

//            //// User code to fill out the properties for each resource
//            //engine.GetResourcePropertyValues(resources);

//            //return resources;
//            return null;
//        }


//        /// <summary>
//        /// Retrieve all property names for a specified resource
//        /// </summary>
//        /// <param name="resource">A resource, either an object or a collection</param>
//        /// <param name="depth">Depth specifier</param>
//        /// <returns></returns>
//        public PropertyStatus[] PropName(string resource, WebDAVdotNet.Headers.Depth depth)
//        {
//            //string[] resourceHrefs = _engine.GetResources(resource, depth);
//            //Dictionary<string, List<Property>> properties = null;
//            //_engine.GetAllResourceProperties(resourceHrefs, out properties);

//            ////Process the output
//            //List<PropertyStatus> stats = new List<PropertyStatus>();
//            //foreach (KeyValuePair<string, List<Property>> kvp in properties)
//            //{
//            //    foreach (Property p in kvp.Value)
//            //    {
//            //        PropertyStatus ps = new PropertyStatus(p);
//            //        ps.PropStatus = PropertyStatus.Status.OK;
//            //        stats.Add(ps);
//            //    }
//            //}

//            //return stats.ToArray();
//            return null;
//        }

//        ///// <summary>
//        ///// 
//        ///// </summary>
//        ///// <param name="resource"></param>
//        ///// <param name="depth"></param>
//        ///// <returns></returns>
//        //public PropertyStatus[] AllProp(string resource, WebDAVdotNet.Headers.Depth depth)
//        //{
//        //    return AllProp(resource, depth, null);
//        //}

//        //public PropertyStatus[] AllProp(string resource, WebDAVdotNet.Headers.Depth depth, Property[] includeProperties)
//        //{
//        //    return null;
//        //}

//        //public class Resource
//        //{
//        //    public string href { get; set; }
//        //    public PropertyStatus[] Properties { get; set; }

//        //    public Resource(string href, PropertyStatus[] properties)
//        //    {
//        //        this.href = href;
//        //        this.Properties = properties;
//        //    }

//        //    public int GetHashCode(Resource obj)
//        //    {
//        //        throw new NotImplementedException();
//        //    }
//        //}

//        //public class Property
//        //{
//        //    public string Namespace { get; set; }
//        //    public string Name { get; set; }

//        //    internal Property(string Namespace, string Name)
//        //    {
//        //        this.Name = Name;
//        //        this.Namespace = Namespace;
//        //    }
//        //}

//        //public class PropertyStatus
//        //{
//        //    public enum Status
//        //    {
//        //        OK = 200,
//        //        Unauthorized = 401,
//        //        Forbidden = 403,
//        //        NotFound = 404
//        //    }

//        //    #region Properties
//        //    object _value;

//        //    public Property Property { get; private set; }

//        //    public Status PropStatus { get; internal set; }

//        //    public object PropValue
//        //    {
//        //        get { return _value; }
//        //        set
//        //        {
//        //            _value = value;
//        //            PropStatus = Status.OK;
//        //        }
//        //    }
//        //    #endregion

//        //    #region Constructors

//        //    internal PropertyStatus(Property property)
//        //    {
//        //        PropStatus = Status.NotFound;
//        //        Property = new Property(property.Namespace, property.Name);
//        //    }

//        //    #endregion

//        //    #region Methods

//        //    void Unauthorized()
//        //    {
//        //        PropStatus = Status.Unauthorized;
//        //    }

//        //    void Forbidden()
//        //    {
//        //        PropStatus = Status.Forbidden;
//        //    }

//        //    void NotFound()
//        //    {
//        //        PropStatus = Status.NotFound;
//        //    }
//        //    #endregion

//        //}
//    }
//}
