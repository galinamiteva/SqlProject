

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.ViewModels;

public partial class UpdateCustomerViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    public UpdateCustomerViewModel(IServiceProvider serviceProvider, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        Customer = _customerService.CurrentCustomer;
    }


    [ObservableProperty]
    private CustomerDto customer = new CustomerDto();

    [RelayCommand]

    private async Task UpdateCustomer()
    {
        await _customerService.UpdateCustomerAsync(Customer);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel=_serviceProvider.GetRequiredService<DetailsCustomerViewModel>();
    }


    [RelayCommand]

    private void NavigateToList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel=_serviceProvider.GetRequiredService<CustomerListViewModel>();

    }

    [RelayCommand]
    private async Task UpdateCustomersEmail()
    {
        await _customerService.UpdateCustomersEmailAsync(Customer);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsCustomerViewModel>();
    }


}
