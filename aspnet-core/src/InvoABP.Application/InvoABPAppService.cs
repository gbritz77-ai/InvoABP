using System;
using System.Collections.Generic;
using System.Text;
using InvoABP.Localization;
using Volo.Abp.Application.Services;

namespace InvoABP;

/* Inherit your application services from this class.
 */
public abstract class InvoABPAppService : ApplicationService
{
    protected InvoABPAppService()
    {
        LocalizationResource = typeof(InvoABPResource);
    }
}
