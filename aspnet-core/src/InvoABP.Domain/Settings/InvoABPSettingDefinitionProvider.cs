using Volo.Abp.Settings;

namespace InvoABP.Settings;

public class InvoABPSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(InvoABPSettings.MySetting1));
    }
}
