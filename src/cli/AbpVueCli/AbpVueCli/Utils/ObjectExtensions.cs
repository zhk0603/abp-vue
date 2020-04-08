using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AbpVueCli.Utils
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }
    }
}
