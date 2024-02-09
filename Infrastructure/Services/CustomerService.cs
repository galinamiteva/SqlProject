

using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerService(DataContext context, CustomerRepository customerRepository, RoleService roleService, AddressService addressService, ContactService contactService, AuthService authService )
{
    private readonly CustomerRepository _customerRepository= customerRepository;
    private readonly RoleService _roleService = roleService;
    private readonly AddressService _addressService= addressService;
    private readonly ContactService _contactService= contactService;
    private readonly AuthService _authService= authService;
    private readonly DataContext _context = context;

    public CustomerDto CurrentCustomer { get;  set; } = null!;
    public bool CreateCustomer (CustomerDto customer)
    {
        try
        {
            if(!_customerRepository.Exists(x=>x.Email == customer.Email))
            {
                var roleEntity = _roleService.CreateRole(customer.RoleName);
                var addressEntity = _addressService.CreateAddress(customer.StreetName, customer.City, customer.PostalCode);

                var customerEntity = new CustomerEntity
                {
                    Email = customer.Email,
                    RoleId = roleEntity.Id,
                    AddressId = addressEntity.Id,
                };
                var customerResult = _customerRepository.Create(customerEntity);

                if (customerResult != null)
                {
                    var contactEntity = _contactService.CreateContact(customer.FirstName, customer.LastName, customerResult.Id, customer.PhoneNumber);
                    var authEntity = _authService.CreateAuth(customer.LoginName, customer.Pass, customerResult.Id);
                
                }
                return true;

            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return false;
    }


    public CustomerEntity GetOneCustomerByEmail(string email)
    {
        var customerEntity = _customerRepository.GetOne(x=>x.Email == email);
        return customerEntity;
    }

    public CustomerEntity GetCustomerById(Guid id)
    {
        var customerEntity = _customerRepository.GetOne(x => x.Id == id);
        return customerEntity;
    }

    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        List<CustomerDto> customers = new List<CustomerDto>();

        try
        {
            var result =_customerRepository.GetAll();

            if (result != null)
            {
                foreach (var customer in result)
                {
                    customers.Add(new CustomerDto
                    {
                        Id= customer.Id,
                        FirstName= customer.Contact.FirstName,
                        LastName = customer.Contact.LastName,
                        Email = customer.Email,
                        PhoneNumber = customer.Contact.PhoneNumber,
                        RoleName = customer.Role.RoleName,
                        StreetName = customer.Address.StreetName,
                        City = customer.Address.City,
                        PostalCode = customer.Address.PostalCode,
                        LoginName = customer.Auth.LoginName,
                        Pass = customer.Auth.Pass,
                    });
                }
                return customers;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;

    }


    public async Task <CustomerEntity> UpdateCustomersEmailAsync (CustomerDto updatedCustomer)
    {
        try
        {
            var existingCustomerEntity = await _customerRepository.GetOneAsync(x => x.Id == updatedCustomer.Id);

            if(existingCustomerEntity != null)
            {
                existingCustomerEntity.Email = updatedCustomer.Email;
                await _context.SaveChangesAsync();
                return await _customerRepository.UpdateAsync(x => x.Id == updatedCustomer.Id, existingCustomerEntity);
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }


   


    public async Task<CustomerEntity> UpdateCustomerAsync(CustomerDto updatedCustomer)
    {
        try
        {
            var existingCustomerEntity = await _customerRepository.GetOneAsync(x => x.Id == updatedCustomer.Id);


            if(existingCustomerEntity != null)
            {
                existingCustomerEntity.Role.RoleName = updatedCustomer.RoleName;

                existingCustomerEntity.Address.StreetName = updatedCustomer.StreetName;
                existingCustomerEntity.Address.PostalCode = updatedCustomer.PostalCode; 
                existingCustomerEntity.Address.City = updatedCustomer.City;

                existingCustomerEntity.Contact.FirstName = updatedCustomer.FirstName;
                existingCustomerEntity.Contact.LastName = updatedCustomer.LastName; 
                existingCustomerEntity.Contact.PhoneNumber= updatedCustomer.PhoneNumber;

                existingCustomerEntity.Auth.LoginName = updatedCustomer.LoginName;
                existingCustomerEntity.Auth.Pass=updatedCustomer.Pass;

                existingCustomerEntity.Email= updatedCustomer.Email;
                existingCustomerEntity.Id = updatedCustomer.Id;

                var result = await _customerRepository.UpdateAsync(x => x.Id == updatedCustomer.Id, existingCustomerEntity);  
                return result;


                
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }


    public bool DeleteCustomer(CustomerDto customerDto)
    {
        _customerRepository.Delete(x => x.Email == customerDto.Email);
        return true;
    }

}
