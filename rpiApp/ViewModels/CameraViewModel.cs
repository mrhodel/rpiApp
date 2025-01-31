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
    private readonly IDialogService? _dialogService;
    public CameraViewModel()
    {
    }

    public CameraViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public async void OnStartImagingAsync()
    {
        if (Design.IsDesignMode)
        {
            return;
        }

        // todo: why does this cause an exception in the text editor (screws up syntax highlighting too)
        try
        {
            var result = await _dialogService!.ShowMessageBoxAsync(
                    null,
                    "Camera View is Not implemented", "",
                    MessageBoxButton.Ok,
                    MessageBoxImage.Warning,
                    null);
            Debug.WriteLine($"Dialog result: {result}");
        }
        catch (System.Exception e)
        {
            Debug.WriteLine(e);
        }
    }

    public async void OnGetCameraInfo()
    {
        Guard.IsNotNull(_dialogService);
        var result = await _dialogService.ShowCameraInfoViewAsync(null);
        Debug.WriteLine($"Dialog result: {result}");
    }
}
