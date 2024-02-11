

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels;

public partial class AddCustomerViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;
    private readonly RoleService _roleService;

    [ObservableProperty]
    private ObservableCollection<RoleDto> _roleList = new ObservableCollection<RoleDto>();
    public RoleDto SelectedRole { get; set; } = null!;


    [ObservableProperty]
    private CustomerDto customer = new();


    public AddCustomerViewModel(IServiceProvider serviceProvider, RoleService roleService, CustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
        _roleService = roleService;

        RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());

    }








    [RelayCommand]

    private async Task AddCustomer(CustomerDto customerDto)
    {
        if (SelectedRole != null)
        {
            Customer.RoleName = SelectedRole.RoleName;

        }

        await _customerService.CreateCustomerAsync(customerDto);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
    }

    [RelayCommand]
    private void NavigateToList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CustomerListViewModel>();
    }

}
