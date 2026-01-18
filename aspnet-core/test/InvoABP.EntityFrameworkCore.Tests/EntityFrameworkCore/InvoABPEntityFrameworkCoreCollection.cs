using Xunit;

namespace InvoABP.EntityFrameworkCore;

[CollectionDefinition(InvoABPTestConsts.CollectionDefinitionName)]
public class InvoABPEntityFrameworkCoreCollection : ICollectionFixture<InvoABPEntityFrameworkCoreFixture>
{

}
