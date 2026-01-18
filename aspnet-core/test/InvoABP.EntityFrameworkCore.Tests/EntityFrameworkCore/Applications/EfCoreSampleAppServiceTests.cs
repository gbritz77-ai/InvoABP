using InvoABP.Samples;
using Xunit;

namespace InvoABP.EntityFrameworkCore.Applications;

[Collection(InvoABPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<InvoABPEntityFrameworkCoreTestModule>
{

}
