using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using rpiApp.ViewModels;

namespace rpiApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current?.Services.GetService<MainWindowViewModel>();
        }
    }
}