using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using WPF_SQLSERVER.Abstractions.Interfaces;
using WPF_SQLSERVER.Abstractions;
using WPF_SQLSERVER.Helpers;
using WPF_SQLSERVER.Infraestructure;
using WPF_SQLSERVER.Services.Products;

namespace WPF_SQLSERVER;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();
        ConfigureServices(services, configuration);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Database connection 
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        // Generic Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // Services
        services.AddScoped<ProductService>();

        // Controllers
        services.AddScoped<ProductController>();


        // Main window access
        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
        base.OnStartup(e);
    }
}