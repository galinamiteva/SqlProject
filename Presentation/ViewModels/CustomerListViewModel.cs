

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Presentation.ViewModels;

public partial class CustomerListViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;


    [ObservableProperty]
    private ObservableCollection<CustomerDto> _customerList = new ObservableCollection<CustomerDto>();



    public CustomerListViewModel(IServiceProvider serviceProvider, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        CustomerList = new ObservableCollection<CustomerDto>(_customerService.GetAllCustomers());
    }


    [RelayCommand]

    private void NavigateToAddView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddCustomerViewModel>();

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
    private void NavigateToCustomerDetailView(CustomerDto customer)
    {
        _customerService.SelectedCustomer = customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsCustomerViewModel>();

    }

    [RelayCommand]
    private void NavigateToUpdateContactView(CustomerDto customer)
    {
        _customerService.SelectedCustomer = customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateCustomerViewModel>();

    }


    [RelayCommand]
    private void DCToDetailView(CustomerDto customer)
    {
        _customerService.SelectedCustomer = customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsCustomerViewModel>();
    }


    [RelayCommand]

    private void NavigateToRoles()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<RoleListViewModel>();
    }

    [RelayCommand]
    private void NavigateToAddresses()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddressListViewModel>();
    }
}
