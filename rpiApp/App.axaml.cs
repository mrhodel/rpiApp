using Avalonia;
using Avalonia.Data.Core.Plugins;
using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using Microsoft.Extensions.DependencyInjection;
using rpiApp.Models;
using rpiApp.Services;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;

namespace rpiApp
{
    public partial class App : Application
    {
        public static LoggingLevelSwitch LoggingLevelSwitch { get; set; } = new();

        public override void Initialize()
        {
            // Configure services.
            new ServiceCollection().ConfigureServices();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            // Configure Serilog
            LoggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
            var logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Versions.ApplicationName!, "logfiles", $"{Versions.ApplicationName!}_.log");
            Log.Logger = new LoggerConfiguration()
                                     .MinimumLevel.ControlledBy(LoggingLevelSwitch)
                                     .WriteTo.Debug()
                                     .WriteTo.File(logFile,
                                                    rollingInterval: RollingInterval.Day,
                                                    retainedFileTimeLimit: TimeSpan.FromDays(365),
                                                    retainedFileCountLimit: null,
                                                    flushToDiskInterval: TimeSpan.FromSeconds(5))
                                     .CreateLogger();
            Log.Information($"======= {Versions.ApplicationName} Version {Versions.CurrentVersion} =======");

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