﻿using Infrastructure.Dtos;

using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;

    //Create Address async
    public async Task<AddressDto> CreateAddressAsync(string streetName, string city, string postalCode)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(x => x.StreetName == streetName && x.City == city && x.PostalCode == postalCode);
            addressEntity ??= await _addressRepository.CreateAsync(new AddressEntity { StreetName = streetName, City = city, PostalCode = postalCode });
            
            return new AddressDto {Id=addressEntity.Id, StreetName=addressEntity.StreetName, PostalCode=addressEntity.PostalCode, City=addressEntity.City };
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }



    // Create Address
   


    public AddressEntity CreateAddress(string streetName, string postalCode, string city)
    {
        var result = _addressRepository.GetOne(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
        result ??= _addressRepository.Create(new AddressEntity { StreetName = streetName, PostalCode = postalCode, City = city });

        return new AddressEntity { Id = result.Id, StreetName = result.StreetName, PostalCode = result.PostalCode, City = result.City };
    }


    public IEnumerable<AddressDto> GetAllAddresses()
    {
        var addresses = new List<AddressDto>();
        try
        {
            var result = _addressRepository.GetAll();

            if (result != null)
            {
                foreach (var address in result)
                    addresses.Add(new AddressDto
                    {
                        Id = address.Id,
                        StreetName = address.StreetName,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    });
            }
            return addresses;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }





    public async Task<AddressDto> GetAddressAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            var result = await _addressRepository.GetOneAsync(predicate);
            if (result != null)
                return new AddressDto { Id = result.Id, StreetName = result.StreetName, PostalCode = result.PostalCode, City = result.City };
        }
        catch { }
        return null!;
    }

    public AddressEntity GetAddress(string streetName, string postalCode, string city)
    {
        var addressEntity = _addressRepository.GetOne(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
        return addressEntity;
    }

    public AddressEntity GetAddressById(int Id)
    {
        var addressEntity = _addressRepository.GetOne(x => x.Id == Id);
        return addressEntity;
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
