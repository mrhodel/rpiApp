/*
 * CameraViewModel.cs
 * 1/26/2025 Mike Hodel
*/


//using LibVLCSharp.Shared;

using Avalonia.Controls;
using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using rpiApp.Services;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class CameraViewModel : ViewModelBase
{
    private readonly IDialogService? dialogService;
    public CameraViewModel()
    {
    }

    public CameraViewModel(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    public async void OnStartImagingAsync()
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        var result = await dialogService!.ShowMessageBoxAsync(
                null,
                "Camera View is Not implemented", "",
                MessageBoxButton.Ok,
                MessageBoxImage.Warning,
                null);
        Debug.WriteLine($"Dialog result: {result}");
    }

    public async void OnGetCameraInfo()
    {
        Guard.IsNotNull(dialogService);
        var result = await dialogService.ShowCameraInfoViewAsync(null);
        Debug.WriteLine($"Dialog result: {result}");
    }
}
