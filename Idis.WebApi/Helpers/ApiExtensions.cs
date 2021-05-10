using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Idis.WebApi
{
    public static class ApiExtensions
    {
        public static string JsonPrettify(this string json)
        {
            using var stringReader = new StringReader(json);
            using var stringWriter = new StringWriter();

            var jsonReader = new JsonTextReader(stringReader);
            var jsonWriter = new JsonTextWriter(stringWriter)
            {
                Formatting = Newtonsoft.Json.Formatting.Indented
            };
            jsonWriter.WriteToken(jsonReader);
            return stringWriter.ToString();
        }

        public static OpenApiObject ToOpenApiObject(this object model)
        {
            // Get all properties on the object
            var properties = model.GetType().GetProperties()
                .Where(x => x.CanRead)
                .ToDictionary(x => x.Name, x => x.GetValue(model, null));

            var opi = new OpenApiObject();

            foreach (var prop in properties)
            {
                object value = null;
                value = prop.Value switch
                {
                    null => new OpenApiNull(),
                    int as_int => new OpenApiInteger(as_int),
                    bool as_bool => new OpenApiBoolean(as_bool),
                    byte as_byte => new OpenApiByte(as_byte),
                    float as_float => new OpenApiFloat(as_float),
                    _ => new OpenApiString($"{prop.Value}"),
                };
                opi.Add(prop.Key, value as IOpenApiAny);
            }
            return opi;
        }

        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}