using Avalonia;
using Avalonia.Data.Core.Plugins;
using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using rpiApp.Services;
using System.Linq;

namespace rpiApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            // Configure services.
            ConfigureIocServices.ConfigureServices(); //
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            // Show the main window.
            Ioc.Default.GetService<IDialogService>()?.ShowMainWindowView();

            base.OnFrameworkInitializationCompleted();
        }

        private static void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }

        // ... other code ...
    }
}