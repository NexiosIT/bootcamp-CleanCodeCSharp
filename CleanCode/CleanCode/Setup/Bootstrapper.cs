using CleanCode.Business.Repositories;
using CleanCode.Business.Services;
using CleanCode.Common.Extensions;
using CleanCode.Common.Parsers;
using CleanCode.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanCode.App.Setup;

public class Bootstrapper
{
    public static IServiceProvider GetServiceProvider() => GetServiceCollection().BuildServiceProvider();

    private static IServiceCollection GetServiceCollection()
    {
        var config = GetConfiguration();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<App>();

        SetupConfiguration(serviceCollection, config);
        SetupServiceCollection(serviceCollection);

        return serviceCollection;
    }

    private static void SetupServiceCollection(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBookRepository, BookRepository>();
        serviceCollection.AddScoped<IBookService, BookService>();
        serviceCollection.AddScoped<IBookParser, BookParser>();
    }

    private static void SetupConfiguration(IServiceCollection serviceCollection, IConfigurationRoot config)
    {
        serviceCollection.Configure<AppConfiguration>(Options.DefaultName, options => config.GetSection("App").Bind(options));
        serviceCollection.AddScoped(cfg => cfg.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value);
    }

    private static IConfigurationRoot GetConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.ApplyConfigurationFiles();

        return configurationBuilder.Build();
    }
}
