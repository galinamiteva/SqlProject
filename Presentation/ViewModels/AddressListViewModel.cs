

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels;

public partial class AddressListViewModel : ObservableObject
{

    private readonly AddressService _addressService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<AddressDto> addressList = [];



    public AddressListViewModel(AddressService addressService, IServiceProvider serviceProvider)
    {
        _addressService = addressService;
        _serviceProvider = serviceProvider;

        AddressList = new ObservableCollection<AddressDto>(_addressService.GetAllAddresses());
    }


    [RelayCommand]
    private void NavigateToCustomers()
        
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();   
        mainViewModel.CurrentViewModel=_serviceProvider.GetRequiredService<RoleListViewModel>();
    }



}
