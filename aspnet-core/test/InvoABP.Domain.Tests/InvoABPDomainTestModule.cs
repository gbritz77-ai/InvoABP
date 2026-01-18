using Volo.Abp.Modularity;

namespace InvoABP;

[DependsOn(
    typeof(InvoABPDomainModule),
    typeof(InvoABPTestBaseModule)
)]
public class InvoABPDomainTestModule : AbpModule
{

}
