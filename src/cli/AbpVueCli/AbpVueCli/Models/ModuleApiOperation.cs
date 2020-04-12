using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace AbpVueCli.Models
{
    public class ModuleApiOperation
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public OpenApiOperation Operation { get; set; }

        public ModuleApiOperation()
        {
        }
    }
}