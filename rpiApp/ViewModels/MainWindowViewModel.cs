/*
 * MainWindowViewModel.cs
 * 1/26/2025 Mike Hodel
*/
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.DependencyInjection;
using rpiApp.Services;
using System.Diagnostics;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(PropertiesViewModel? propertiesViewModel,
                                         ChartingViewModel? chartingViewModel,
                                         CameraViewModel? cameraViewModel,
                                         ILedService? ledService) : ViewModelBase
{
    public MainWindowViewModel() : this(null, null, null, null)
    {   /* For XAML previewer */
        this.PropertiesViewModel = Ioc.Default.GetRequiredService<PropertiesViewModel>();
        this.ChartingViewModel = Ioc.Default.GetRequiredService<ChartingViewModel>();
        this.CameraViewModel = Ioc.Default.GetRequiredService<CameraViewModel>();
        this.LedService = Ioc.Default.GetRequiredService<ILedService>();
    }

    public PropertiesViewModel? PropertiesViewModel { get; set; } = propertiesViewModel;
    public ChartingViewModel? ChartingViewModel { get; set; } = chartingViewModel;
    public CameraViewModel? CameraViewModel { get; set; } = cameraViewModel;
    public ILedService? LedService { get; set; } = ledService;

    private object? _selectedItem = null;
    public object? SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;

            // Some logic here
            var item = value as TabItem;
            Debug.WriteLine(item!.Name);
            if (item!.Name == "ExitApp")
            {

                if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                {
                    lifetime.Shutdown();
                }
            }
        }
    }
}
