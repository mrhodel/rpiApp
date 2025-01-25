using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using rpiApp.Services;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel : ViewModelBase, IRecipient<CameraInfoMessage>, IRecipient<LedChangedMessage>, IRecipient<AppClosingMessage>
{
    [ObservableProperty]
    public partial string CameraInfoText { get; set; } = "Waiting for camera info...";

    readonly ICameraService? cameraService;

    public CameraInfoViewModel() { }

    public CameraInfoViewModel(ICameraService cameraService)
    {
        this.cameraService = cameraService;
        WeakReferenceMessenger.Default.Register<CameraInfoMessage>(this);
        WeakReferenceMessenger.Default.Register<LedChangedMessage>(this);
        WeakReferenceMessenger.Default.Register<AppClosingMessage>(this);
    }

    public void OnGetCameraInfo()
    {
        cameraService!.GetInfoAsync();
    }

    public void Receive(CameraInfoMessage message)
    {
        CameraInfoText = message.Value;
    }

    public void Receive(LedChangedMessage message)
    {
        Debug.WriteLine("Led changed: " + message.Value);
        cameraService!.SetLeds();
    }

    public void Receive(AppClosingMessage message)
    {
        cameraService!.CameraParameters.LedIndicators = 0;
        cameraService!.SetLeds();
        Debug.WriteLine(message.Value);
    }
}
