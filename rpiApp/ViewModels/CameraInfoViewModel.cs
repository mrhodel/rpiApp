using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using rpiApp.Services;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel : ViewModelBase, IRecipient<CameraInfoMessage>
{
    [ObservableProperty]
    public partial string CameraInfoText { get; set; } = "Waiting for camera info...";

    readonly ICameraService? cameraService;

    public CameraInfoViewModel() { }

    public CameraInfoViewModel(ICameraService cameraService)
    {
        this.cameraService = cameraService;
        WeakReferenceMessenger.Default.Register<CameraInfoMessage>(this);
    }

    public void OnGetCameraInfo()
    {
        cameraService!.GetInfoAsync();
    }

    public void Receive(CameraInfoMessage message)
    {
        CameraInfoText = message.Value;
    }
}
