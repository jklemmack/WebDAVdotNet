using System;
using System.Collections.Generic;
using System.Linq;

namespace WebDAVdotNet
{

    public static class Enums
    {
        public static IEnumerable<T> Get<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }


    }

    public static class Extensions
    {
        public static void Add(this IDictionary<PropertyName, PropertyValue> dict, PropertyName key, object value)
        {
            dict.Add(key, new PropertyValue(key, value));
        }
    }
}
