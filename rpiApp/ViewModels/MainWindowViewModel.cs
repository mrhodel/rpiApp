using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(IDialogService dialogService) : ViewModelBase
{
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    public MainWindowViewModel() : this(null) { }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

    public string Greeting { get; } = "Welcome to Avalonia!";

    public void CameraParametersSetCommandAsync()
    {
        var vm = dialogService?.CreateViewModel<CameraInfoViewModel>();
        Guard.IsNotNull(vm);
        dialogService?.ShowDialogAsync(this, vm);
    }
}
