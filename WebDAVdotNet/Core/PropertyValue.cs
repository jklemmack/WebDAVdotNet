using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDAVdotNet
{
    public class PropertyValue
    {
        public enum Status
        {
            OK = 200,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404
        }


        private PropertyName _propertyName;
        private object _value;
        private Status _propertyStatus;

        public PropertyName PropertyName { get { return _propertyName; } set { _propertyName = value; } }
        public object Value { get { return _value; } set { _value = value; } }
        public Status PropertyStatus { get { return _propertyStatus; } internal set { _propertyStatus = value; } }

        public PropertyValue()
        {
            _propertyName = new PropertyName();
            _value = null;
        }

        public PropertyValue(PropertyName name)
        {
            _propertyName = name;
            PropertyStatus = Status.NotFound;
        }

        public PropertyValue(PropertyName name, object value)
        {
            _propertyName = name;
            _value = value;
            PropertyStatus = Status.OK;
        }

        void Unauthorized()
        {
            PropertyStatus = Status.Unauthorized;
        }

        void Forbidden()
        {
            PropertyStatus = Status.Forbidden;
        }

        void NotFound()
        {
            PropertyStatus = Status.NotFound;
        }




    }
}
