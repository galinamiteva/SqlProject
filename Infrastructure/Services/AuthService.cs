

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
                LoginName = loginName,
                Pass = pass,
                CustomerId = customerId
            };
            var result = _authRepository.Create(authEntity);
            if (result != null)
                return result;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;

    }

    public AuthEntity GetAuthByCustomerName(string loginName)
    {
        var authEntity = _authRepository.GetOne(x => x.LoginName == loginName);
        return authEntity;
    }

    public AuthEntity GetAuthById(Guid customerId)
    {
        var authEntity = _authRepository.GetOne(x => x.CustomerId == customerId);
        return authEntity;
    }

    public IEnumerable<AuthEntity> GetAutss()
    {
        var auths = new List<AuthEntity>();
        return auths;
    }

    public AuthEntity UpdateAuth(AuthEntity authEntity)
    {
        var updatedAuth = _authRepository.Update(x => x.CustomerId == authEntity.CustomerId, authEntity);
        return updatedAuth;
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

    public void DeleteAuth(Guid id)
    {
        _authRepository.Delete(x => x.CustomerId == id);
    }

}
