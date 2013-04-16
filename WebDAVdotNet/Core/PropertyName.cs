using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDAVdotNet
{
    public struct PropertyName : IEquatable<PropertyName>
    {
        private string _namespace;
        private string _name;

        public string Namespace { get { return _namespace; } set { _namespace = value; } }
        public string Name { get { return _name; } set { _name = value; } }

        public static PropertyName CREATIONDATE = new PropertyName("DAV:", "creationdate");
        public static PropertyName DISPLAYNAME = new PropertyName("DAV:", "displayname");
        public static PropertyName CONTENTLANGUAGE = new PropertyName("DAV:", "getcontentlanguage");
        public static PropertyName CONTENTLENGTH = new PropertyName("DAV:", "getcontentlength");
        public static PropertyName CONTENTTYPE = new PropertyName("DAV:", "getcontenttype");
        public static PropertyName ETAG = new PropertyName("DAV:", "getetag");
        public static PropertyName LASTMODIFIED = new PropertyName("DAV:", "getlastmodified");
        public static PropertyName RESOURCETYPE = new PropertyName("DAV:", "resourcetype");


        public static PropertyName LOCKDISCOVERY = new PropertyName("DAV:", "lockdiscovery");
        public static PropertyName SUPPORTEDLOCK = new PropertyName("DAV:", "supportedlock");

        public PropertyName(string PropertyNamespace, string PropertyName)
        {
            _name = PropertyName;
            _namespace = PropertyNamespace;
        }

        public static implicit operator PropertyName(string name)
        {
            int split = -1;
            if ((split = name.IndexOf(':')) > 0)
            {
                return new PropertyName(name.Substring(0, split), name.Substring(split, name.Length - split));
            }
            return null;
        }


        public bool Equals(PropertyName other)
        {
            return (this.Name == other.Name && this.Namespace == other.Namespace);
        }
    }
}
