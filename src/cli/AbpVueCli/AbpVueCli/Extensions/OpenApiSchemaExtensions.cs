using Esprima.Ast;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace AbpVueCli.Extensions
{
    public static class OpenApiSchemaExtensions
    {
        public static string ToJson(this OpenApiSchema apiSchema)
        {
            var modelObject = new JObject();
            foreach (var property in apiSchema.Properties)
            {
                modelObject.Add(property.Key, GetDefaultValue(property.Value));
            }

            return modelObject.ToString();
        }

        private static JToken GetDefaultValue(OpenApiSchema propertySchema)
        {
            JToken val = null;
            switch (propertySchema.Type)
            {
                case "string":
                    val = string.Empty;
                    break;
                case "array":
                    val = new JArray();
                    break;
                case "integer":
                case "number":
                    val = 0;
                    break;
            }
            return val;
        }
    }
}
