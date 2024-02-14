

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels;

public partial class UpdateCustomerViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CustomerService _customerService;

    private readonly RoleService _roleService;


    [ObservableProperty]
    private ObservableCollection<RoleDto> _roleList = new ObservableCollection<RoleDto>();

    public RoleDto SelectedRole { get; set; } = null!;





    public UpdateCustomerViewModel(IServiceProvider serviceProvider, CustomerService customerService, RoleService roleService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
        _roleService = roleService;


        RoleList = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());
        Customer = _customerService.SelectedCustomer;
    }

    [ObservableProperty]
    private CustomerDto customer = new();




    [RelayCommand]

    private async Task Update()
    {
        if (SelectedRole != null)
        {
            Customer.RoleName = SelectedRole.RoleName;

        }

        await _customerService.UpdateCustomerAsync(Customer);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsCustomerViewModel>();

    }



    [RelayCommand]

    private void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<DetailsCustomerViewModel>();

    }
}
