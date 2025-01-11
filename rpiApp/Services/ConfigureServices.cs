using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia;
using HanumanInstitute.MvvmDialogs.Avalonia.MessageBox;
using Microsoft.Extensions.DependencyInjection;
using rpiApp.ViewModels;

namespace rpiApp.Services
{
    internal static class ConfigureIocServices
    {
        internal static void ConfigureServices()
        {
            DialogManager dm = new(
                    viewLocator: new ViewLocator(),
                    dialogFactory: new DialogFactory().AddDialogHost().AddMessageBox(MessageBoxMode.Window))
            {
                AllowConcurrentDialogs = true
            };

            Ioc.Default.ConfigureServices(
            new ServiceCollection()
                .AddSingleton<IDialogService>(new DialogService(dm, viewModelFactory: x => Ioc.Default.GetService(x)))
                .AddTransient<MainWindowViewModel>()
                .AddTransient<CameraInfoViewModel>()
                .BuildServiceProvider());
        }
    }
}
