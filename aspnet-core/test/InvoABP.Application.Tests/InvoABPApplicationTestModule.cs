using Volo.Abp.Modularity;

namespace InvoABP;

[DependsOn(
    typeof(InvoABPApplicationModule),
    typeof(InvoABPDomainTestModule)
)]
public class InvoABPApplicationTestModule : AbpModule
{

}
