using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbpVueCli
{
    public static class CliPaths
    {
        private static readonly string AbpRootPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".abp-vue");

        public static string Log => Path.Combine(AbpRootPath, "logs");
    }
}
