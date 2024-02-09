

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;


namespace Presentation.ViewModels;

public partial class MainViewModel:ObservableObject
{
    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    private readonly IServiceProvider _serviceProvider;

   public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
    }
}
