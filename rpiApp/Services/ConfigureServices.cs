/*
 * ConfigureServices.cs
 * 1/26/2025 Mike Hodel
*/
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
                dialogFactory: new DialogFactory().AddDialogHost().AddMessageBox(MessageBoxMode.Popup))
        {
            AllowConcurrentDialogs = true
        };

        services.AddTransient<MainWindowViewModel>()
                .AddTransient<PropertiesViewModel>()
                .AddTransient<TextEditViewModel>()
                .AddTransient<CameraInfoViewModel>()
                .AddTransient<CameraViewModel>()
                .AddTransient<ChartingViewModel>()
                .AddSingleton<IPropertiesService, PropertiesService>()
                .AddSingleton<IDialogService>(new DialogService(dm, viewModelFactory: x => Ioc.Default.GetService(x)));

        if (IsRunningOnRaspberryPiOS())
        {
            services.AddSingleton<ILedService, LedServiceRpi>();
            services.AddSingleton<ICameraService, CameraServiceRpi>();
        }
        else
        {
            services.AddSingleton<ILedService, LedServicePC>();
            services.AddSingleton<ICameraService, CameraServicePC>();
        }

        Ioc.Default.ConfigureServices(services.BuildServiceProvider());
    }

    public static bool IsRunningOnRaspberryPiOS()
    {
        string osDescription = RuntimeInformation.OSDescription.ToLower();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return osDescription.Contains("raspbian") ||
                   osDescription.Contains("raspberry") ||
                   osDescription.Contains("debian");
        }
        return false;
    }
}
