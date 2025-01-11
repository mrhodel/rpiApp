﻿using Avalonia.Media;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.Avalonia.DialogHost;
using rpiApp.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace rpiApp.Services;

public static class DialogExtensions
{
    public static void ShowMainWindowView(this IDialogService dialog)
    {
        var viewModel = dialog.CreateViewModel<MainWindowViewModel>();
        dialog.Show(null, viewModel);
    }

    public static async Task<bool?> ShowCameraInfoViewAsync(this IDialogService dialog, INotifyPropertyChanged ownerViewModel)
    {
        var viewModel = dialog.CreateViewModel<CameraInfoViewModel>();
        var settings = new DialogHostSettings(viewModel)
        {
            DialogMargin = new Avalonia.Thickness(20),
            DisableOpeningAnimation = false,
            OverlayBackground = Brushes.LightBlue,
            CloseOnClickAway = true
        };
        await dialog.ShowDialogHostAsync(ownerViewModel, settings);

        return viewModel.DialogResult;
    }
}
