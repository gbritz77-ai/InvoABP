using Volo.Abp.Modularity;

namespace InvoABP;

/* Inherit from this class for your domain layer tests. */
public abstract class InvoABPDomainTestBase<TStartupModule> : InvoABPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
