namespace AbpVueCli.Commands
{
    public class GenerateCommandOptionBasic
    {
        public string ModulePrefix { get; set; }
        public string Module { get; set; }
        public string Directory { get; set; }
        public bool Overwrite { get; set; }
        public string OutputFolder { get; set; }
    }

    public class CrudGenerateCommandOptionBasic : GenerateCommandOptionBasic
    {

    }
}