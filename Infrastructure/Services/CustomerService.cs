

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

            //Create Async
    public async Task <CustomerEntity> CreateCustomerAsync (CustomerDto customerDto)
    {
        try
        {
            if(!_customerRepository.Exists(x=>x.Email == customerDto.Email))
            {
                var roleEntity = await _roleService.CreateRoleAsync (customerDto.RoleName);
                var addressEntity = _addressService.CreateAddress(customerDto.StreetName, customerDto.PostalCode, customerDto.City);


                    var customerEntity = new CustomerEntity
                    {
                        Email = customerDto.Email,
                        RoleId = roleEntity.Id,
                        AddressId = addressEntity.Id

                    };

                    var customerResult = await _customerRepository.CreateAsync (customerEntity);
                if (customerResult != null)
                {
                    var contactEntity = _contactService.CreateContact(customerDto.FirstName, customerDto.LastName, customerDto.PhoneNumber!, customerResult.Id);
                    var authEntity = _authService.CreateAuth(customerDto.LoginName, customerDto.Pass, customerDto.Id);
                }
                return customerResult!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

                                      //Create

    public CustomerEntity CreateCustomer (CustomerDto customerDto)
    {
        try
        {
            if (!_customerRepository.Exists(x => x.Email == customerDto.Email))
            {
                var roleEntity = _roleService.CreateRole(customerDto.RoleName);
                var addressEntity = _addressService.CreateAddress(customerDto.StreetName, customerDto.PostalCode, customerDto.City);

                var customerEntity = new CustomerEntity
                {
                    Email = customerDto.Email,
                    RoleId = roleEntity.Id,
                    AddressId = addressEntity.Id
                };

                var customerResult = _customerRepository.Create(customerEntity);
                if (customerResult != null)
                {
                    var contactEntity = _contactService.CreateContact(customerDto.FirstName, customerDto.LastName, customerDto.PhoneNumber!, customerResult.Id);
                    var authEntity = _authService.CreateAuth(customerDto.LoginName, customerDto.Pass, customerResult.Id);
                }
                return customerResult!;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

                        //GetAll
    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        var customers = new List<CustomerDto>();
        try
        {
            var result = _customerRepository.GetAll();
            if(result != null)
            {
                foreach ( var customer in result)
                {
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
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }


                //GetOne By Id

    public CustomerEntity GetCustomerById(CustomerDto customerDto)
    {
        var customerEntity = _customerRepository.GetOne(x => x.Id == customerDto.Id);
        return customerEntity;
    }

    //GetOne By Email

    public CustomerEntity GetCustomerByEmail(CustomerDto customerDto)
    {
        var customerEntity = _customerRepository.GetOne(x => x.Email == customerDto.Email);
        return customerEntity;
    }


                 //Update Async Email
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



                //Update Async 
    public async Task<CustomerEntity> UpdateCustomerAsync(CustomerDto updatedCustomer)
    {
        try
        {
            var entity =await _customerRepository.GetOneAsync(x=>x.Id == updatedCustomer.Id);
            if(entity != null)
            {
                var roleEntity = _roleService.CreateRole(updatedCustomer.RoleName);
                var addressEntity = _addressService.CreateAddress(updatedCustomer.StreetName, updatedCustomer.PostalCode, updatedCustomer.City);

                entity.AddressId = addressEntity.Id;
                entity.Email= updatedCustomer.Email;
                entity.RoleId = roleEntity.Id;

                var result =await _customerRepository.UpdateAsync(x=>x.Id == updatedCustomer.Id, entity);
                if (result != null) 
                
                    return new CustomerEntity
                    {
                        Id = updatedCustomer.Id,
                        Email = updatedCustomer.Email,
                        AddressId = addressEntity.Id,
                        RoleId = roleEntity.Id,
                    };

                var contactEntity = _contactService.CreateContact(updatedCustomer.FirstName, updatedCustomer.LastName, updatedCustomer.PhoneNumber!, updatedCustomer.Id);
                var authEntity = _authService.CreateAuth(updatedCustomer.LoginName, updatedCustomer.Pass, updatedCustomer.Id);
                
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
        }

        return null!;
    }

            
    
                //Delete
    public void DeleteCustomer(CustomerDto customerDto)
    {
        _customerRepository.Delete(x=>x.Email == customerDto.Email);
    }





}
