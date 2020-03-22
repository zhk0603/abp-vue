using Volo.Abp.Settings;

namespace Abp.VueTemplate.Settings
{
    public class VueTemplateSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(VueTemplateSettings.MySetting1));
        }
    }
}
