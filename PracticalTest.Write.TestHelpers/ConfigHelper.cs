// PartnerManagement - PM.Api.SmartRepair.Test.Helper - ConfigHelper.cs
// created on 2022/05/10

using Microsoft.Extensions.Configuration;

namespace PracticalTest.Write.TestHelpers
{
  public static class ConfigHelper
  {
    public static IConfiguration GetConfig()
    {
      var test = Directory.GetCurrentDirectory();
     return  new ConfigurationBuilder()
        .SetBasePath( Directory.GetCurrentDirectory() )
        .AddJsonFile( $"appsettings.Development", true, true )
        .Build();
    }


  }
}