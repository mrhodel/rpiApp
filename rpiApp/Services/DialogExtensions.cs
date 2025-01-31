/*
 * DialogExtensions.cs
 * 1/26/2025 Mike Hodel
*/

using CommunityToolkit.Diagnostics;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia.DialogHost;
using rpiApp.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace rpiApp.Services;

public static class DialogExtensions
{
    private static MainWindowViewModel? _mainViewModel;
    public static void ShowMainWindowView(this IDialogService dialog)
    {
        _mainViewModel = dialog.CreateViewModel<MainWindowViewModel>();
        dialog.Show(null, _mainViewModel);
    }

    public static async Task<bool?> ShowCameraInfoViewAsync(this IDialogService dialog, INotifyPropertyChanged? ownerViewModel)
    {
        var viewModel = dialog.CreateViewModel<CameraInfoViewModel>();
        var settings = new DialogHostSettings(viewModel)
        {
            DialogMargin = new Avalonia.Thickness(20),
            DisableOpeningAnimation = false,
            CloseOnClickAway = true
        };
        // If the calling view model is for a window then use that window as the owner, otherwise use the main view.
        Guard.IsNotNull(_mainViewModel);
        await dialog.ShowDialogHostAsync(ownerViewModel: (ownerViewModel is not null) ? ownerViewModel : _mainViewModel, settings);
        return viewModel.DialogResult;
    }
}
