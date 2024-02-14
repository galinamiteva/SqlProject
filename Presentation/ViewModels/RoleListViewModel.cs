

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Presentation.ViewModels;

public partial class RoleListViewModel: ObservableObject
{
    private readonly RoleService _roleService;
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    [ObservableProperty]
    private RoleDto role = new();

    [ObservableProperty]
    private ObservableCollection<RoleDto> roleList = [];



    [ObservableProperty]
    private ObservableCollection<CustomerDto>  _customerList= new ObservableCollection<CustomerDto>();

    public RoleListViewModel(RoleService roleService, CustomerService customerService, IServiceProvider serviceProvider)
    {
        _roleService = roleService;
        _serviceProvider = serviceProvider;
        _customerService = customerService;

        RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());
        CustomerList = new ObservableCollection<CustomerDto>(_customerService.GetAllCustomers());
    }


    [RelayCommand]
    private async Task AddRole()
    {

        if (string.IsNullOrWhiteSpace(Role.RoleName))
        {
            MessageBox.Show("Rolename can not be empty, please enter a role", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }


        await _roleService.CreateRoleAsync(Role.RoleName!);

        RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());
        Role = new();

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<RoleListViewModel>();
    }


    [RelayCommand]
    private void DeleteRole(RoleDto role)
    {
        if (CustomerList.Any(customer => customer.RoleName == role.RoleName))
        {
            MessageBoxResult result = MessageBox.Show($"This is registered customer with role: {role.RoleName}. If you continue the customer will  be removed.", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                _roleService.DeleteRole(role);
                RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());
            }
        }
        else
        {
            _roleService.DeleteRole(role);
            RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());
        }
    }

    [RelayCommand]
    private void NavigateToCustomers()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
    }

    [RelayCommand]
    private void NavigateToAddresses()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddressListViewModel>();
    }
}
