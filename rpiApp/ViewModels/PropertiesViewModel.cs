using CommunityToolkit.Mvvm.DependencyInjection;
using rpiApp.Models;
using rpiApp.Services;

namespace rpiApp.ViewModels;

public partial class PropertiesViewModel : ViewModelBase
{
    public Properties Properties { get; set; }

    public PropertiesViewModel(IPropertiesService properties)
    {
        Properties = properties.Properties;
    }

    public PropertiesViewModel()
    {   /* For XAML previewer */
        Properties = Ioc.Default.GetRequiredService<IPropertiesService>().Properties;
    }
}
