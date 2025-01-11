using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel(IDialogService? dialogService) : ViewModelBase, IModalDialogViewModel, ICloseable, IViewClosing
{
    public CameraInfoViewModel() : this(null) { }

    public bool? DialogResult { get; set; } = false;
    public event EventHandler? RequestClose;

    public void OnClose()
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
        e.Cancel = false;

        if (DialogResult == true)
        {
            await dialogService.ShowMessageBoxAsync(null, "Camera parameters set successfully.", "Success");
        }
        else
        {
            await dialogService.ShowMessageBoxAsync(null, "Camera parameters not set.", "Success");
        }
    }
}
