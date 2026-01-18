using Volo.Abp.Modularity;

namespace InvoABP;

public abstract class InvoABPApplicationTestBase<TStartupModule> : InvoABPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
