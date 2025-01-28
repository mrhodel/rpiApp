using HanumanInstitute.MvvmDialogs.Avalonia;

namespace rpiApp;

public class ViewLocator : ViewLocatorBase
{
    protected override string GetViewName(object viewModel) => viewModel.GetType().FullName!.Replace("ViewModel", "View");
}
