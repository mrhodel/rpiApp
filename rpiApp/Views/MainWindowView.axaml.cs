using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using System;

namespace rpiApp.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();

        // Handle the Closing event of the window
        Closed += MainWindow_Closed;
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new AppClosingMessage("App Closing"));
    }
}
