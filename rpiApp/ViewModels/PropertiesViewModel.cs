/*
 * PropertiesViewModel.cs
 * 1/26/2025 Mike Hodel
*/
using CommunityToolkit.Mvvm.DependencyInjection;
using rpiApp.Models;
using rpiApp.Services;

namespace rpiApp.ViewModels;

public partial class PropertiesViewModel : ViewModelBase
{
    public Properties Properties { get; set; }

    public PropertiesViewModel(IPropertiesService propertiesService)
    {
        Properties = propertiesService.Properties;
    }

    public PropertiesViewModel()
    {   /* For XAML previewer */
        Properties = Ioc.Default.GetRequiredService<IPropertiesService>().Properties;
    }
}
