using Xunit;

namespace PracticalTest.Write.Tests.Integration.Common;

[CollectionDefinition("Test collection")]
public class SharedCollectionFixture:ICollectionFixture<BlogApiFactory>
{
    
}