

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Presentation.ViewModels;

public partial class DetailsCustomerViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    public DetailsCustomerViewModel(IServiceProvider serviceProvider, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        Customer = _customerService.SelectedCustomer;

    }

    [ObservableProperty]
    private CustomerDto customer = new();



    [RelayCommand]
    private void NavigateToListView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
    }

    [RelayCommand]

    private void DeleteCustomer(CustomerDto customer)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Please confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _customerService.DeleteCustomer(customer);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
        }


    }


    [RelayCommand]

    private void NavigateToUpdateCustomerView()
    {
        _customerService.SelectedCustomer = Customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateCustomerViewModel>();
    }

}
