using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CraftsApi.Auth
{
    public static class TypeExtensions
    {
        private static readonly ConcurrentDictionary<Type, PropertyDescriptorCollection> _cache = new ConcurrentDictionary<Type, PropertyDescriptorCollection>();
        private static readonly HashSet<Type> _simpleTypes = new HashSet<Type>
        {
            typeof(string),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Enum),
            typeof(decimal),
            typeof(Guid)
        };
        private static readonly Type _enumerableType = typeof(IEnumerable);
        private static readonly Type _formattableType = typeof(IFormattable);

        /// <summary>
        /// Get a collection of all property names for a given object.
        /// </summary>
        /// <param name="obj">Object to get property names for.</param>
        public static IEnumerable<string> GetPropertyNames(this object obj, bool excludeVirtual = false)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            foreach (PropertyDescriptor prop in obj.GetProperties(excludeVirtual))
            {
                if (excludeVirtual)
                {
                    // TODO: [NDL ]Can I get accessors from PropertyDescriptor instead?
                    var isVirtual = obj.GetType().GetProperty(prop.Name).GetAccessors()[0].IsVirtual;
                    if (isVirtual)
                    {
                        continue;
                    }
                }

                yield return prop.Name;
            }
        }

        /// <summary>
        /// Get a collection of all properties for a given object.
        /// </summary>
        /// <param name="obj">Object to get properties for.</param>
        public static IEnumerable<PropertyDescriptor> GetProperties(this object obj, bool excludeVirtual = false)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var type = obj.GetType();
            PropertyDescriptorCollection properties;

            if (!_cache.ContainsKey(type))
            {
                properties = TypeDescriptor.GetProperties(obj);
                _cache.AddOrUpdate(type, properties, (Type a, PropertyDescriptorCollection b) => { return properties; });
            }
            else
            {
                properties = _cache[type];
            }

            foreach (PropertyDescriptor prop in properties)
            {
                if (excludeVirtual)
                {
                    var isVirtual = type.GetProperty(prop.Name).GetAccessors()[0].IsVirtual;
                    if (isVirtual)
                    {
                        continue;
                    }
                }

                yield return prop;
            }
        }

        /// <summary>
        /// Determines whether the type is a simple type (text, number, date, enum, etc).
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsPrimitive || _simpleTypes.Contains(type) || type.IsEnum;
        }

        /// <summary>
        /// Determines whether the type is a collection or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsArray || type.IsGenericType && type.GetInterfaces().Contains(_enumerableType);
        }

        /// <summary>
        /// Determines whether the type is a value type that cannot be formattet or not.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNonFormatableStruct(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsValueType && !type.IsPrimitive && !type.GetInterfaces().Contains(_formattableType);
        }

        public static Type GetElementTypeOfEnumerable(this IEnumerable enumerable)
        {
            // Borrowed from https://benohead.com/c-get-element-type-enumerable-reflection/

            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            Type[] interfaces = enumerable.GetType().GetInterfaces();
            Type elementType = (from i in interfaces
                                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                select i.GetGenericArguments()[0]).FirstOrDefault();

            // Peek at the first element in the list if you couldn't determine the element type
            if (elementType == null || elementType == typeof(object))
            {
                object firstElement = enumerable.Cast<object>().FirstOrDefault();
                if (firstElement != null)
                {
                    elementType = firstElement.GetType();
                }
            }

            return elementType;
        }
    }
}
