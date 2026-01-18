using InvoABP.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InvoABP.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class InvoABPController : AbpControllerBase
{
    protected InvoABPController()
    {
        LocalizationResource = typeof(InvoABPResource);
    }
}
