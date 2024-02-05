

using Infrastructure.Entitites;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AddressService
{
    private readonly AddressRepository _addressRepository;
    public AddressService(AddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }


    public AddressEntity CreateAddress(string streetName, string city, int postalCode)
    {
        var addressEntity = _addressRepository.GetOne(x => x.StreetName == streetName && x.City == city && x.PostalCode == postalCode);
        addressEntity ??= _addressRepository.Create(new AddressEntity { StreetName = streetName, City = city, PostalCode = postalCode });
        return addressEntity;
    }


    public AddressEntity GetAddressByStreetName(string streetName)
    {
        var addressEntity = _addressRepository.GetOne(x=>x.StreetName == streetName);
        return addressEntity;

    }

    public AddressEntity GetAddressById(int id)
    {
        var addressEntity = _addressRepository.GetOne(x => x.Id == id);
        return addressEntity;
    }



    public IEnumerable<AddressEntity> GetAllAddresses()
    {
        var addresses = _addressRepository.GetAll();    
        return addresses;
    }

    public AddressEntity UpdateAddress(AddressEntity addressEntity)
    {
        var updatedAddressEntity = _addressRepository.Update(x => x.Id == addressEntity.Id, addressEntity);
        return updatedAddressEntity;
    }


    public void DeleteAddress(int id)
    {
        _addressRepository.Delete(x => x.Id == id);
    }
}
