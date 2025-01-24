using CommunityToolkit.Mvvm.DependencyInjection;
using HanumanInstitute.MvvmDialogs;
using rpiApp.Services;
using System;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class CameraSettingsViewModel(ICameraService? cameraService) : ViewModelBase, IViewLoaded
{
    public ICameraService? CameraService { get; set; } = cameraService;
    public CameraSettingsViewModel() : this(null)
    {   /* For XAML previewer */
        CameraService = Ioc.Default.GetRequiredService<ICameraService>();
    }

    public void OnLoaded()
    {
        CameraService!.GetInfoAsync();
        Debug.WriteLine("0x" + Convert.ToString((int)CameraService!.CameraParameters.LedIndicators, 16));
        Debug.WriteLine("0b" + Convert.ToString((int)CameraService!.CameraParameters.LedIndicators, 2));
    }

    public void OnButton()
    {
        Debug.WriteLine($"{CameraService!.CameraParameters.LedIndicators}");
    }
}
