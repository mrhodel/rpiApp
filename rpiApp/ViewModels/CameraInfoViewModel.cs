using HanumanInstitute.MvvmDialogs;
using System;

namespace rpiApp.ViewModels;

public partial class CameraInfoViewModel() : ViewModelBase, IModalDialogViewModel, ICloseable
{
    public bool? DialogResult { get; set; }
    public event EventHandler? RequestClose;

    public void OnClose()
    {
        DialogResult = true;
        RequestClose?.Invoke(this, EventArgs.Empty);
    }
}
