﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.Diagnostics;

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
            Debug.WriteLine(item!.Name);
        }
    }
}
