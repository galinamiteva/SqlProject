

using Infrastructure.Dtos;
using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ContactService(ContactRepository contactRepository)
{
    private readonly ContactRepository _contactRepository = contactRepository;


                   //Create
    public async Task<ContactDto> CreateContactAsync(string firstName, string lastName, Guid customerId, string? phoneNumber)
    {
        try
        {
           var result = await _contactRepository.GetOneAsync(x=>x.CustomerId == customerId);
            result ??= await _contactRepository.CreateAsync(new ContactEntity
            {
                FirstName = firstName,
                LastName = lastName,
                CustomerId = customerId,
                PhoneNumber = phoneNumber
            });

            return new ContactDto { CustomerId = result.CustomerId, FirstName = result.FirstName, LastName = result.LastName, PhoneNumber = result.PhoneNumber };

        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }

    public ContactEntity CreateContact(string firstName, string lastName, string phoneNumber, Guid customerId)
    {

        var contactInformationEntity = new ContactEntity
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            CustomerId = customerId,
        };

        return _contactRepository.Create(contactInformationEntity);
    }


    public ContactEntity GetOneContact(ContactDto contactDto)
    {
        var contactEntity = _contactRepository.GetOne(x => x.FirstName == contactDto.FirstName && x.LastName== contactDto.LastName&&x.PhoneNumber==contactDto.PhoneNumber);
        return contactEntity;
    }
    public ContactEntity GetOneContactByCustomerId(Guid customerId)
    {
        var contactEntity = _contactRepository.GetOne(x=>x.CustomerId == customerId);
        return contactEntity;
    }

    public IEnumerable<ContactEntity> GetAllContacts()
    {
        var contacts = _contactRepository.GetAll();
        return contacts;   
    }

    public ContactEntity UpdateContact (ContactEntity contactEntity)
    {
        var updatedEntity = _contactRepository.Update(x => x.CustomerId == contactEntity.CustomerId, contactEntity);
        return updatedEntity;
    
    }


    public void DeleteContact(Guid ContactId)
    {
        _contactRepository.Delete(x=>x.CustomerId == ContactId);
    }


}
