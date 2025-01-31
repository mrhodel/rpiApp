/*
 * MainWindowViewModel.cs
 * 1/26/2025 Mike Hodel
*/
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.DependencyInjection;
using rpiApp.Services;
using Serilog;

namespace rpiApp.ViewModels;

public partial class MainWindowViewModel(PropertiesViewModel? propertiesViewModel,
                                         ChartingViewModel? chartingViewModel,
                                         TextEditViewModel? textEditViewModel,
                                         CameraViewModel? cameraViewModel,
                                         ILedService? ledService) : ViewModelBase
{
    public MainWindowViewModel() : this(null, null, null, null, null)
    {   /* For XAML previewer */
        PropertiesViewModel = Ioc.Default.GetRequiredService<PropertiesViewModel>();
        ChartingViewModel = Ioc.Default.GetRequiredService<ChartingViewModel>();
        TextEditViewModel = Ioc.Default.GetRequiredService<TextEditViewModel>();
        CameraViewModel = Ioc.Default.GetRequiredService<CameraViewModel>();
        LedService = Ioc.Default.GetRequiredService<ILedService>();
    }

    public PropertiesViewModel? PropertiesViewModel { get; set; } = propertiesViewModel;
    public ChartingViewModel? ChartingViewModel { get; set; } = chartingViewModel;
    public TextEditViewModel? TextEditViewModel { get; set; } = textEditViewModel;
    public CameraViewModel? CameraViewModel { get; set; } = cameraViewModel;
    public ILedService? LedService { get; set; } = ledService;

    private object? _selectedItem = null;
    public object? SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;

            var item = value as TabItem;
            Log.Information($"Tab {item!.Name} selected");
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
