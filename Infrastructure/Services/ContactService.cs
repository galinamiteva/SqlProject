

using Infrastructure.Dtos;
using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ContactService(ContactRepository contactRepository)
{
    private readonly ContactRepository _contactRepository = contactRepository;


    //Create
    public async Task<ContactDto> CreateContactAsync(string firstName, string lastName, string phoneNumber, Guid customerId)
    {
        try
        {
            var result = await _contactRepository.GetOneAsync(x => x.CustomerId == customerId);
            result ??= await _contactRepository.CreateAsync(new ContactEntity { FirstName = firstName, LastName = lastName, PhoneNumber = phoneNumber, CustomerId = customerId });

            return new ContactDto { CustomerId = result.CustomerId, FirstName = result.FirstName, LastName = result.LastName, PhoneNumber = result.PhoneNumber };
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }



    public ContactEntity CreateContact(string firstName, string lastName, string phoneNumber, Guid customerId)
    {

        var contactEntity = new ContactEntity
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            CustomerId = customerId,
        };

        return _contactRepository.Create(contactEntity);
    }


    public async Task<bool> UpdateContactAsync(Guid customerId, string firstName, string lastName, string? phoneNumber)
    {
        try
        {
            var newContact = await _contactRepository.UpdateOneAsync(new ContactEntity
            {
                CustomerId = customerId,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            });
            return newContact != null;
        }
        catch
        {

        }
        return false!;
    }



    public void DeleteContact(Guid ContactId)
    {
        _contactRepository.Delete(x=>x.CustomerId == ContactId);
    }


}
