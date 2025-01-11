using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using rpiApp.Services;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(IDialogService? dialogService) : ViewModelBase
{
    public MainWindowViewModel() : this(null) { }

    public string Greeting { get; } = "Welcome to Avalonia!";

    public async void CameraParametersSetCommandAsync()
    {
        Guard.IsNotNull(dialogService);
        var result = await dialogService.ShowCameraInfoViewAsync(ownerViewModel: this);
        Debug.WriteLine(result);
    }
}
