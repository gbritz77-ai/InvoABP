using InvoABP.Samples;
using Xunit;

namespace InvoABP.EntityFrameworkCore.Domains;

[Collection(InvoABPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<InvoABPEntityFrameworkCoreTestModule>
{

}
