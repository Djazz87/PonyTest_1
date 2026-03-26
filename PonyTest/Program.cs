using Avalonia;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PonyTest.DB;
using PonyTest.ViewModels;
using PonyTest.Views;

namespace PonyTest;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables();
            }).
            ConfigureServices((c, s) =>
        {
            s.Configure<DataBaseConection>(c.Configuration.GetSection("DataBaseConection"));
            s.AddTransient<MainWindow>();
            s.AddTransient<MainWindowViewModel>();
            s.AddTransient<TestRepository>();
            s.AddTransient<TestWindow>();
            s.AddTransient<TestWindowViewModel>();
            s.AddTransient<QuestionRepository>();
            s.AddTransient<ResultRepository>();

        }).Build();
        BuildAvaloniaApp(host.Services)
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp(IServiceProvider serviceProvider)
        => AppBuilder.Configure(()=> new App(serviceProvider))
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}