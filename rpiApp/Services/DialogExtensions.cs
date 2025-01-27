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
    private static MainWindowViewModel? mainView;
    public static void ShowMainWindowView(this IDialogService dialog)
    {
        mainView = dialog.CreateViewModel<MainWindowViewModel>();
        dialog.Show(null, mainView);
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
        Guard.IsNotNull(mainView);
        // If the calling view model is for a window then use that as the owner, otherwise use the main view.
        await dialog.ShowDialogHostAsync((ownerViewModel is not null) ? ownerViewModel : mainView, settings);
        return viewModel.DialogResult;
    }
}
