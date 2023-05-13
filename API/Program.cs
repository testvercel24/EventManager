using Microsoft.Extensions.Hosting;
namespace API
{
  [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
  public class Program
  {
    static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(WebHostBuilder =>
          {
            WebHostBuilder.UseStartup<Startup>();
          });
    }
  }
}
