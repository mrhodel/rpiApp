using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.MessageBox;
using Microsoft.Extensions.DependencyInjection;
using rpiApp.ViewModels;
using System.Runtime.InteropServices;

namespace rpiApp.Services;
internal static class ConfigureIocServices
{
    public static void ConfigureServices(this IServiceCollection services)  // Extension method
    {
        DialogManager dm = new(
                viewLocator: new ViewLocator(),
                dialogFactory: new DialogFactory().AddDialogHost().AddMessageBox(MessageBoxMode.Window))
        {
            AllowConcurrentDialogs = true
        };

        services.AddTransient<MainWindowViewModel>()
                .AddTransient<PropertiesViewModel>()
                .AddTransient<CameraInfoViewModel>()
                .AddTransient<CameraViewModel>()
                .AddTransient<ChartingViewModel>()
                .AddSingleton<IPropertiesService, PropertiesService>()
                .AddSingleton<IDialogService>(new DialogService(dm, viewModelFactory: x => Ioc.Default.GetService(x)));

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            services.AddSingleton<ILedService, LedServiceLinux>();
            services.AddSingleton<ICameraService, CameraServiceLinux>();
        }
        else
        {
            services.AddSingleton<ILedService, LedServicePC>();
            services.AddSingleton<ICameraService, CameraServicePC>();
        }

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }
}
