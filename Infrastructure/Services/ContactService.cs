

using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ContactService
{
    private readonly ContactRepository _contactRepository;

    public ContactService (ContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }  
    

    public ContactEntity CreateContact(string firstName, string lastName, Guid customerId, string? phoneNumber)
    {
        try
        {
            var contactEntity = new ContactEntity()
            {
                FirstName = firstName,
                LastName = lastName,
                CustomerId = customerId,
                PhoneNumber = phoneNumber

            };
            var result = _contactRepository.Create(contactEntity);
            if (result != null)
            
                return result;
            

        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
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


    public void DeleteContact(Guid contactId)
    {
        _contactRepository.Delete(x=>x.CustomerId == contactId);
    }


}
