

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Windows.Themes;
using System.Collections.ObjectModel;
using System.Windows;


namespace Presentation.ViewModels;

public partial class CustomerListViewModel: ObservableObject
{

    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    public CustomerListViewModel(IServiceProvider serviceProvider, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        Customers = new ObservableCollection<CustomerDto>(_customerService.GetAllCustomers());
    }

    [ObservableProperty]
    private ObservableCollection<CustomerDto> _customers = new ObservableCollection<CustomerDto>();

    [RelayCommand]

    private void NavigateToAddCustomer()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddCustomerViewModel>();

    }

    [RelayCommand]
    private void NavigateToDetail(CustomerDto customer)
    {
        _customerService.CurrentCustomer = customer;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
       mainViewModel.CurrentViewModel=_serviceProvider.GetRequiredService<DetailsCustomerViewModel>();  

    }

    [RelayCommand]

    private void NavigateToDelete(CustomerDto customerDto)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Please confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _customerService.DeleteCustomer(customerDto);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
        }

    }

}
