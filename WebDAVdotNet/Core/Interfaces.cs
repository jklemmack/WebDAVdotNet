using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace WebDAVdotNet
{
    public interface IHierarchyItem
    {
        DateTime? CreatedDate { get; set; }
        DateTime? LastModified { get; set; }
        string Name { get; set; }
        string Path { get; set; }

        IDictionary<PropertyName, PropertyValue> Properties { get; }
    }

    public interface IContent
    {
        long ContentLength { get; set; }
        string ContentType { get; set; }
        string ETag { get; set; }

        /// <summary>
        /// Reads content from the item in the repository and writes it to the specified output stream
        /// </summary>
        /// <param name="output"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        long Read(Stream output, long startIndex, long length);

        /// <summary>
        /// Reads content from the repository and writes it to the specified output stream
        /// </summary>
        /// <param name="output"></param>
        long Read(Stream output);

        /// <summary>
        /// Writes the contents of a stream to the object in the repository
        /// </summary>
        /// <param name="input"></param>
        /// <param name="contentType"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        bool Write(Stream input, string contentType, long startIndex, long length);
    }

    public interface IItemCollection
    {
        string ResourceType { get; set; }

        /// <summary>
        /// Returns the children items of this object
        /// </summary>
        /// <returns></returns>
        IEnumerable<IHierarchyItem> GetChildren();

        /// <summary>
        /// Returns the children items of this object.  Each child should include the specified properties
        /// </summary>
        /// <returns></returns>
        IEnumerable<IHierarchyItem> GetChildren(IList<PropertyName> properties);
    }

    public abstract class IFile : IHierarchyItem, IContent
    {
        private IDictionary<PropertyName, PropertyValue> _properties = new Dictionary<PropertyName, PropertyValue>();

        public virtual DateTime? CreatedDate
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.CREATIONDATE, out value))
                {
                    return (DateTime)value.Value;
                }
                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.CREATIONDATE))
                    _properties.Add(PropertyName.CREATIONDATE, new PropertyValue(PropertyName.CREATIONDATE, value));
                else
                    _properties[PropertyName.CREATIONDATE].Value = value;
            }
        }

        public virtual DateTime? LastModified
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.LASTMODIFIED, out value))
                {
                    return (DateTime)value.Value;
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.LASTMODIFIED))
                    _properties.Add(PropertyName.LASTMODIFIED, new PropertyValue(PropertyName.LASTMODIFIED, value));
                else
                    _properties[PropertyName.LASTMODIFIED].Value = value;
            }
        }

        public virtual string Name
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.DISPLAYNAME, out value))
                {
                    if (value.Value is string)
                        return (string)value.Value;
                    else
                        return value.Value.ToString();
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.DISPLAYNAME))
                    _properties.Add(PropertyName.DISPLAYNAME, new PropertyValue(PropertyName.DISPLAYNAME, value));
                else
                    _properties[PropertyName.DISPLAYNAME].Value = value;
            }
        }

        public abstract string Path
        {
            get;
            set;
        }

        public virtual IDictionary<PropertyName, PropertyValue> Properties
        {
            get
            {
                return _properties;
            }
            internal set
            {
                _properties = value;
            }
        }

        public virtual long ContentLength
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.CONTENTLENGTH, out value))
                {
                    long length = 0;
                    if (value.Value is long)
                        return (long)value.Value;
                    if (value.Value is string)
                        if (long.TryParse((string)value.Value, out length))
                            return length;
                }

                return 0;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.CONTENTLENGTH))
                    _properties.Add(PropertyName.CONTENTLENGTH, new PropertyValue(PropertyName.CONTENTLENGTH, value));
                else
                    _properties[PropertyName.CONTENTLENGTH].Value = value;
            }
        }

        public virtual string ContentType
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.CONTENTTYPE, out value))
                {
                    if (value.Value is string)
                        return (string)value.Value;
                    else
                        return value.Value.ToString();
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.CONTENTTYPE))
                    _properties.Add(PropertyName.CONTENTTYPE, new PropertyValue(PropertyName.CONTENTTYPE, value));
                else
                    _properties[PropertyName.CONTENTTYPE].Value = value;
            }
        }

        public virtual string ETag
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.ETAG, out value))
                {
                    if (value.Value is string)
                        return (string)value.Value;
                    else
                        return value.Value.ToString();
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.ETAG))
                    _properties.Add(PropertyName.ETAG, new PropertyValue(PropertyName.ETAG, value));
                else
                    _properties[PropertyName.ETAG].Value = value;
            }
        }

        public abstract long Read(Stream output, long startIndex, long length);

        public abstract long Read(Stream output);

        public abstract bool Write(Stream input, string contentType, long startIndex, long length);

        public IFile()
        {
            _properties.Add(PropertyName.CONTENTTYPE, "application/octet-stream");
        }
    }

    public abstract class IFolder : IItemCollection, IHierarchyItem
    {

        protected IDictionary<PropertyName, PropertyValue> _properties = new Dictionary<PropertyName, PropertyValue>();

        public virtual DateTime? CreatedDate
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.CREATIONDATE, out value))
                {
                    return (DateTime)value.Value;
                }
                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.CREATIONDATE))
                    _properties.Add(PropertyName.CREATIONDATE, new PropertyValue(PropertyName.CREATIONDATE, value));
                else
                    _properties[PropertyName.CREATIONDATE].Value = value;
            }
        }

        public virtual DateTime? LastModified
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.LASTMODIFIED, out value))
                {
                    return (DateTime)value.Value;
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.LASTMODIFIED))
                    _properties.Add(PropertyName.LASTMODIFIED, new PropertyValue(PropertyName.LASTMODIFIED, value));
                else
                    _properties[PropertyName.LASTMODIFIED].Value = value;
            }
        }

        public virtual string Name
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.DISPLAYNAME, out value))
                {
                    if (value.Value is string)
                        return (string)value.Value;
                    else
                        return value.Value.ToString();
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.DISPLAYNAME))
                    _properties.Add(PropertyName.DISPLAYNAME, new PropertyValue(PropertyName.DISPLAYNAME, value));
                else
                    _properties[PropertyName.DISPLAYNAME].Value = value;
            }
        }

        public abstract string Path
        {
            get;
            set;
        }

        public virtual string ResourceType
        {
            get
            {
                PropertyValue value = null;
                if (Properties.TryGetValue(PropertyName.RESOURCETYPE, out value))
                {
                    if (value.Value is string)
                        return (string)value.Value;
                    else
                        return value.Value.ToString();
                }

                return null;
            }
            set
            {
                if (!_properties.ContainsKey(PropertyName.RESOURCETYPE))
                    _properties.Add(PropertyName.RESOURCETYPE, new PropertyValue(PropertyName.RESOURCETYPE, value));
                else
                    _properties[PropertyName.RESOURCETYPE].Value = value;
            }
        }

        public virtual IDictionary<PropertyName, PropertyValue> Properties
        {
            get
            {
                return _properties;
            }
            internal set
            {
                _properties = value;
            }
        }

        public abstract IEnumerable<IHierarchyItem> GetChildren();

        public abstract IEnumerable<IHierarchyItem> GetChildren(IList<PropertyName> properties);

        public IFolder()
        {
            XElement resourceType = new XElement(XNamespace.Get("DAV:") + "collection");
            _properties.Add(PropertyName.RESOURCETYPE, new PropertyValue(PropertyName.RESOURCETYPE, resourceType));
        }
    }

}
