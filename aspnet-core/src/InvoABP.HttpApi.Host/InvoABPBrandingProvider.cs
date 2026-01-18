using Microsoft.Extensions.Localization;
using InvoABP.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace InvoABP;

[Dependency(ReplaceServices = true)]
public class InvoABPBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InvoABPResource> _localizer;

    public InvoABPBrandingProvider(IStringLocalizer<InvoABPResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
