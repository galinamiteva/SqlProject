

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Presentation.ViewModels;

public partial class RoleListViewModel:ObservableObject
{
    private readonly RoleService _roleService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private RoleDto roleDto = new();

    [ObservableProperty]
    private ObservableCollection<RoleDto> roleList = [];

    public RoleListViewModel (RoleService roleService, IServiceProvider serviceProvider)
    {
        _roleService = roleService;
        _serviceProvider = serviceProvider;

        RoleList = new ObservableCollection<RoleDto> (_roleService.GetAllRoles());
    }


    [RelayCommand]
    private async Task AddRole()
    {
        await _roleService.CreateRoleAsync(RoleDto.RoleName!);

        RoleList= new ObservableCollection<RoleDto> (_roleService.GetAllRoles());
        RoleDto= new ();

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel= _serviceProvider.GetRequiredService<RoleListViewModel>();   
    }


    [RelayCommand]
    private void DeleteRole(RoleDto role)
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to remove this user?", "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _roleService.DeleteRole(role);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<RoleListViewModel>();
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
