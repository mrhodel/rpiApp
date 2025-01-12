using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HanumanInstitute.MvvmDialogs;
using rpiApp.Models;
using rpiApp.Services;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(IDialogService? dialogService) : ViewModelBase, IViewLoaded, IRecipient<CameraInfoMessage>
{
    public MainWindowViewModel() : this(null) { /* For XAML previewer */ }

    public void OnLoaded()
    {
        WeakReferenceMessenger.Default.Register<CameraInfoMessage>(this);
    }

    [ObservableProperty]
    public partial string Greeting { get; set; } = "Waiting for Camera Parameters...";

    public async void CameraParametersSetCommandAsync()
    {
        Guard.IsNotNull(dialogService);
        var result = await dialogService.ShowCameraInfoViewAsync(ownerViewModel: this);
        Debug.WriteLine(result);
    }

    public void Receive(CameraInfoMessage message)
    {
        Greeting = message.Value;
    }

}
