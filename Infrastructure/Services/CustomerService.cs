

using Infrastructure.Context;
using Infrastructure.Dtos;
using Infrastructure.Entitites;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;
    private readonly RoleService _roleService;
    private readonly AddressService _addressService;
    private readonly AuthService _authService;
    private readonly ContactService _contactService;


    public CustomerService(CustomerRepository customerRepository, RoleService roleService, AddressService addressService, AuthService authService, ContactService contactService)
    {
        _customerRepository = customerRepository;
        _roleService = roleService;
        _addressService = addressService;
        _authService = authService;
        _contactService = contactService;
    }

    public CustomerDto SelectedCustomer { get; set; } = null!;



    //Create 


    public async Task<CustomerEntity> CreateCustomerAsync(CustomerDto customer)
    {
        try
        {
            if (!_customerRepository.Exists(x => x.Email == customer.Email))
            {
                var roleEntity = await _roleService.CreateRoleAsync(customer.RoleName);
                var addressEntity = _addressService.CreateAddress(customer.StreetName, customer.PostalCode, customer.City);

                var customerEntity = new CustomerEntity
                {
                    Email = customer.Email,
                    RoleId = roleEntity.Id,
                    AddressId = addressEntity.Id
                };

                var customerResult = await _customerRepository.CreateAsync(customerEntity);
                if (customerResult != null)
                {
                    var contactInformationEntity = _contactService.CreateContact(customer.FirstName, customer.LastName, customer.PhoneNumber!, customerResult.Id);
                    var authenticationEntity = _authService.CreateAuth(customer.LoginName, customer.Pass, customerResult.Id);
                }
                return customerResult!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }




    public CustomerEntity CreateCustomer(CustomerDto customer)
    {
        try
        {
            if (!_customerRepository.Exists(x => x.Email == customer.Email))
            {
                var roleEntity = _roleService.CreateRole(customer.RoleName);
                var addressEntity = _addressService.CreateAddress(customer.StreetName, customer.PostalCode, customer.City);

                var customerEntity = new CustomerEntity
                {
                    Email = customer.Email,
                    RoleId = roleEntity.Id,
                    AddressId = addressEntity.Id
                };

                var customerResult = _customerRepository.Create(customerEntity);

                if (customerResult != null)
                {
                    var contactEntity = _contactService.CreateContact(customer.FirstName, customer.LastName, customer.PhoneNumber!, customerResult.Id);
                    var authEntity = _authService.CreateAuth(customer.LoginName, customer.Pass, customerResult.Id);
                }
                return customerResult!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    //public async Task<CustomerEntity> UpdateCustomerAsync(CustomerDto updatedCustomer)
    //{
    //    try
    //    {
    //        var entity = await _customerRepository.GetOneAsync(x => x.Id == updatedCustomer.Id);
    //        if (entity != null)
    //        {
    //            var roleEntity = _roleService.CreateRole(updatedCustomer.RoleName);
    //            var addressEntity = _addressService.CreateAddress(updatedCustomer.StreetName, updatedCustomer.PostalCode, updatedCustomer.City);

    //            entity.Email = updatedCustomer.Email;
    //            entity.AddressId = addressEntity.Id;
    //            entity.RoleId = roleEntity.Id;

    //            var customerEntity = new CustomerEntity
    //            {
    //                Id = updatedCustomer.Id,
    //                Email = updatedCustomer.Email,
    //                AddressId = addressEntity.Id,
    //                RoleId = roleEntity.Id,
    //            };

    //            var result = await _customerRepository.UpdateAsync(x => x.Id == updatedCustomer.Id, entity);

    //            var contactInformationEntity = await _contactService.UpdateContactAsync(updatedCustomer.Id, updatedCustomer.FirstName, updatedCustomer.LastName, updatedCustomer.PhoneNumber);
    //            var authenticationEntity = await _authService.UpdateAuthAsync(updatedCustomer.Id, updatedCustomer.LoginName, updatedCustomer.Pass);

    //            return result;
    //        }
    //    }
    //    catch { }
    //    return null!;
    //}

    //Get All Customers
    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        var customers = new List<CustomerDto>();
        try
        {
            var result = _customerRepository.GetAll();

            if (result != null)
            {
                foreach (var customer in result)
                    customers.Add(new CustomerDto
                    {
                        Id = customer.Id,
                        FirstName = customer.Contact.FirstName,
                        LastName = customer.Contact.LastName,
                        Email = customer.Email,
                        RoleName = customer.Role.RoleName,
                        PhoneNumber = customer.Contact.PhoneNumber,
                        StreetName = customer.Address.StreetName,
                        City = customer.Address.City,
                        PostalCode = customer.Address.PostalCode,
                        LoginName = customer.Auth.LoginName,
                        Pass = customer.Auth.Pass,
                    });
            }
            return customers;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;


    }

    // Get One Customer 
   
    public CustomerEntity GetCustomerById(CustomerDto customer)
    {
        var customerEntity = _customerRepository.GetOne(x => x.Id == customer.Id);
        return customerEntity;
    }

    
    public CustomerEntity GetCustomerByEmail(CustomerDto customer)
    {
        var customerEntity = _customerRepository.GetOne(x => x.Email == customer.Email);
        return customerEntity;
    }


    public async Task<CustomerEntity> UpdateCustomerEmailAsync(CustomerDto updatedCustomer)
    {
        try
        {
            var existingCustomerEntity = await _customerRepository.GetOneAsync(x => x.Id == updatedCustomer.Id);

            if (existingCustomerEntity != null)
            {
                existingCustomerEntity.Email = updatedCustomer.Email;
                return await _customerRepository.UpdateAsync(x => x.Id == updatedCustomer.Id, existingCustomerEntity);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }




    //Update Custoemr

    public async Task<CustomerEntity> UpdateCustomerAsync(CustomerDto updatedCustomer)
    {
        try
        {
            var entity = await _customerRepository.GetOneAsync(x => x.Id == updatedCustomer.Id);
            if (entity != null)
            {
                var roleEntity = _roleService.CreateRole(updatedCustomer.RoleName);
                var addressEntity = _addressService.CreateAddress(updatedCustomer.StreetName, updatedCustomer.PostalCode, updatedCustomer.City);

                entity.Email = updatedCustomer.Email;
                entity.AddressId = addressEntity.Id;
                entity.RoleId = roleEntity.Id;

                var customerEntity = new CustomerEntity
                {
                    Id = updatedCustomer.Id,
                    Email = updatedCustomer.Email,
                    AddressId = addressEntity.Id,
                    RoleId = roleEntity.Id,
                };

                var result = await _customerRepository.UpdateAsync(x => x.Id == updatedCustomer.Id, entity);

                    var contactInformationEntity = await _contactService.UpdateContactAsync(updatedCustomer.Id, updatedCustomer.FirstName, updatedCustomer.LastName, updatedCustomer.PhoneNumber);
                    var authenticationEntity = await _authService.UpdateAuthAsync(updatedCustomer.Id, updatedCustomer.LoginName, updatedCustomer.Pass);

                return result;
            }
        }
        catch { }
        return null!;
    }

    //Delete customer
    

    //public void DeleteCustomer(Guid id)
    //{
    //    _customerRepository.Delete(x => x.Id == id);
    //}

    public bool DeleteCustomer(CustomerDto customer)
    {
        _customerRepository.Delete(x => x.Email == customer.Email);
        return true;
    }
}
