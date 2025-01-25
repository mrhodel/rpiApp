using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using rpiApp.Models;
using System;
using System.ComponentModel;

namespace rpiApp.Views;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();

        // Handle the Closing event of the window
        this.Closing += MainWindow_Closing;
        this.Closed += MainWindow_Closed;
    }

    private void MainWindow_Closed(object? sender, EventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new AppClosingMessage("App Closing"));
    }

    private void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new AppClosingMessage("App Closing"));

        // Perform any necessary cleanup or saving operations here, 
        // such as saving user preferences, closing connections, etc.

        // Example: Display a confirmation dialog
        //var result = MessageBox.Show("Are you sure you want to exit?", "Confirmation",
        //    MessageBoxButtons.YesNo, MessageBoxImage.Question);

        //// Cancel the closing event if the user chooses "No"
        //if (result == MessageBoxResult.No)
        //{
        //    e.Cancel = true;
        //}
    }
}