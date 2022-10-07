using Microsoft.Extensions.Configuration;

namespace CleanCode.Common.Extensions;

public static class BootstrapperExtensions
{
    public static void ApplyConfigurationFiles(this IConfigurationBuilder configurationBuilder) => configurationBuilder
                                                                                                   .SetBasePath(Directory.GetCurrentDirectory())
                                                                                                   .AddJsonFile("Configuration/appsettings.json", false, true);
}
