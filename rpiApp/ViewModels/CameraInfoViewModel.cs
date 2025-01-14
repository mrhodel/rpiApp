using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using rpiApp.Models;
using rpiApp.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel(IDialogService? dialogService, ICameraService? cameraService) : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosing
{
    public CameraInfoViewModel() : this(null, null) { /* For XAML previewer */  }

    public CameraParameters CameraParameters { get; set; } = new("Camera Parameters");

    public bool? DialogResult { get; set; } = false;
    public event EventHandler? RequestClose;

    public void OnAccept()
    {
        DialogResult = true;
        RequestClose?.Invoke(this, EventArgs.Empty);
    }

    public void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;

        Debug.WriteLine(Convert.ToString((int)CameraParameters.Flags, 2));
        Debug.WriteLine(Convert.ToString((int)CameraParameters.Flags, 16));
    }

    public async Task OnClosingAsync(CancelEventArgs e)
    {
        Guard.IsNotNull(dialogService);

        var result = await dialogService.ShowMessageBoxAsync(null, "Are you sure you want to close?", "Close", MessageBoxButton.YesNoCancel);
        if (result is not (null or false))
        {
            e.Cancel = false;
        }

        if (e.Cancel || DialogResult != true)
        {
            DialogResult = false;
        }
        else
        {
            cameraService?.GetInfoAsync();
        }
    }
}
