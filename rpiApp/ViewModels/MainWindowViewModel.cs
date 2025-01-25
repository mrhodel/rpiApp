using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using System;
using System.Diagnostics;
using System.Threading;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(CameraSettingsViewModel? cameraSettingsViewModel,
                                         CameraInfoViewModel? cameraInfoViewModel,
                                         CameraViewModel? cameraViewModel) : ViewModelBase
{
    public MainWindowViewModel() : this(null, null, null)
    {   /* For XAML previewer */
        this.CameraSettingsViewModel = Ioc.Default.GetRequiredService<CameraSettingsViewModel>();
        this.CameraInfoViewModel = Ioc.Default.GetRequiredService<CameraInfoViewModel>();
        this.CameraViewModel = Ioc.Default.GetRequiredService<CameraViewModel>();
    }

    [ObservableProperty]
    public partial string Greeting { get; set; } = "Waiting for Camera Info...";
    public CameraSettingsViewModel? CameraSettingsViewModel { get; set; } = cameraSettingsViewModel;
    public CameraInfoViewModel? CameraInfoViewModel { get; set; } = cameraInfoViewModel;
    public CameraViewModel? CameraViewModel { get; set; } = cameraViewModel;

    private object? _selectedItem = null;
    public object? SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;

            // Some logic here
            var item = value as TabItem;

            if (item!.Name == "ExitApp")
            {
                WeakReferenceMessenger.Default.Send(new AppClosingMessage("App Closing"));
                Thread.Sleep(1000);
                Environment.Exit(0);
            }

            Debug.WriteLine(item!.Name);
        }
    }
}
