namespace Abp.VueTemplate.Permission
{
    public static class PermissionDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Permission";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Permission";
    }
}
