﻿

using Infrastructure.Entitites;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AuthService
{
    private readonly AuthRepository _authRepository;

    public AuthService (AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }   

    public AuthEntity CreateAuth(string loginName, string pass, Guid customerId)
    {
        try
        {
            var authEntity = new AuthEntity()
            {
               CustomerId= customerId,
               LoginName= loginName,
               Pass= pass,
            };
            var result = _authRepository.Create(authEntity);
            if (result != null)
            
                return result;
            

        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }

    public AuthEntity GetOneAuthByLoginName( string loginName)
    {
        var authEntity = _authRepository.GetOne(x => x.LoginName == loginName);
        return authEntity;

    }

    public AuthEntity GetOneAuthByCustomerId(Guid customerId)
    {
        var authEntity = _authRepository.GetOne(x => x.CustomerId == customerId);
        return authEntity;

    }
    public IEnumerable<AuthEntity> GetAllAuths()
    {
        var authentications = _authRepository.GetAll();
        return authentications;
    }

    public IEnumerable<AuthEntity> GetRoles()
    {
        var authentications = new List<AuthEntity>();
        return authentications;
    }
    public AuthEntity UpdateRole(AuthEntity authEntity)
    {
        var updatedAuth = _authRepository.Update(x => x.CustomerId == authEntity.CustomerId, authEntity);
        return updatedAuth;
    }
    public AuthEntity UpdateAuth(AuthEntity authEntity)
    {
        var updateAuthEntity = _authRepository.Update(x => x.CustomerId==authEntity.CustomerId, authEntity);
        return updateAuthEntity;
    }


    public async Task<bool> UpdateAuthAsync(Guid customerId, string loginName, string pass)
    {
        try
        {
            var newAuthentication = await _authRepository.UpdateOneAsync(new AuthEntity
            {
                CustomerId = customerId,
                LoginName = loginName,
                Pass = pass

            });
            return newAuthentication != null;
        }
        catch
        {

        }
        return false!;
    }

    public void DeleteRole(Guid id)
    {
        _authRepository.Delete(x => x.CustomerId == id);
    }

    public void DeleteAuth( Guid customerId)
    {
        _authRepository.Delete(x=> x.CustomerId==customerId);   
    }

}
