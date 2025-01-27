/*
 * CameraInfoViewModel.cs
 * 1/26/2025 Mike Hodel
*/
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using HanumanInstitute.MvvmDialogs;
using rpiApp.Models;
using rpiApp.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel : ViewModelBase, IRecipient<CameraInfoMessage>, IModalDialogViewModel, ICloseable, IViewClosing
{
    [ObservableProperty]
    public partial string CameraInfoText { get; set; } = "Waiting for camera info...";
    public bool? DialogResult { get; set; } = false;
    public event EventHandler? RequestClose;

    public CameraInfoViewModel() { }

    public CameraInfoViewModel(ICameraService cameraService)
    {
        WeakReferenceMessenger.Default.Register<CameraInfoMessage>(this);
        cameraService.GetInfoAsync();
    }

    public void OnCloseButton()
    {
        DialogResult = true;
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    public void Receive(CameraInfoMessage message)
    {
        CameraInfoText = message.Value;
    }

    public void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
    }

    public async Task OnClosingAsync(CancelEventArgs e)
    {
        e.Cancel = false;
        await Task.CompletedTask;
    }
}
