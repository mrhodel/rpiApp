using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using rpiApp.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel(IDialogService? dialogService, ICameraService? cameraService) : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosing
{
    public CameraInfoViewModel() : this(null, null) { /* For XAML previewer */  }

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
