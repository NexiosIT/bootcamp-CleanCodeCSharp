using CleanCode.App.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace CleanCode.App;

internal class Program
{
    private static void Main()
    {
        var serviceProvider = Bootstrapper.GetServiceProvider();
        serviceProvider.GetRequiredService<App>().Run();
    }
}
