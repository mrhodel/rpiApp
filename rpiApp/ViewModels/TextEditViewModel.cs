/*
 * TextEditViewModel.cs
 * 1/26/2025 Mike Hodel
*/
using AvaloniaEdit;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using CommunityToolkit.Mvvm.ComponentModel;
using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FileSystem;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using rpiApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace rpiApp.ViewModels;

public partial class TextEditViewModel : ViewModelBase/*, ReactiveObject*/
{
    public ObservableCollection<ThemeViewModel> AllThemes { get; set; } = [];
    private ThemeViewModel _selectedTheme;

    public ThemeViewModel SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            //this.RaiseAndSetIfChanged(ref _selectedTheme, value);
            //_textMateInstallation.SetTheme(_registryOptions.LoadTheme(value.ThemeName));
        }
    }

    readonly IDialogService? _dialogService;
    public ObservableCollection<string> Paths { get; private set; } = [];

    [ObservableProperty]
    public partial TextDocument? ScriptText { get; set; } = new();

    public TextEditViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    public TextEditViewModel()
    {

    }

    public void CopyMouseCommmand(TextArea textArea)
    {
        ApplicationCommands.Copy.Execute(null, textArea);
    }

    public void CutMouseCommand(TextArea textArea)
    {
        ApplicationCommands.Cut.Execute(null, textArea);
    }

    public void PasteMouseCommmand(TextArea textArea)
    {
        ApplicationCommands.Paste.Execute(null, textArea);
    }

    public void SelectAllMouseCommmand(TextArea textArea)
    {
        ApplicationCommands.SelectAll.Execute(null, textArea);
    }

    // Undo Status is not given back to disable it's item in ContextFlyout; therefore it's not being used yet.
    public void UndoMouseCommmand(TextArea textArea)
    {
        ApplicationCommands.Undo.Execute(null, textArea);
    }

    public async Task OnOpenFileButtonAsync()
    {
        var startLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Versions.ApplicationName!);
        var dialogStorageFolder = new DesktopDialogStorageFolder(startLocation);
        var createdFolder = await dialogStorageFolder.CreateFolderAsync("scripts").ConfigureAwait(false);

        if (createdFolder is DesktopDialogStorageFolder newFolder)
        {
            dialogStorageFolder = newFolder;
        }
        else
        {
            // Handle the case where the folder creation failed or returned a different type
            throw new InvalidOperationException("Failed to create the scripts folder.");
        }

        var settings = GetSettings(multiple: false, dialogStorageFolder);
        var result = await _dialogService!.ShowOpenFileDialogAsync(null, settings);
        Paths.Clear();
        if (result?.Path != null)
        {
            Paths.Add(result.Path.LocalPath);
        }

        if (Paths.Count >= 1)
        {
            // Open stream reader from the first file
            var file = new DesktopDialogStorageFile(Paths[0]);
            using var streamReader = new StreamReader(await file.OpenReadAsync());

            // Reads all the content of file
            ScriptText = new TextDocument(await streamReader.ReadToEndAsync());
            Debug.WriteLine(ScriptText.Text);
        }
    }

    private static OpenFileDialogSettings GetSettings(bool multiple, IDialogStorageFolder startLocation)
    {
        return new()
        {
            Title = multiple ? "Open multiple scripts" : "Open script",
            SuggestedFileName = "script.py",
            SuggestedStartLocation = startLocation,
            Filters = [new("Python", ["py"])]
        };
    }
}
