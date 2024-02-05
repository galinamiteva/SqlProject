

using Infrastructure.Entitites;
using Infrastructure.Repositories;
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
            {
                return result;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: " + ex.Message); }
        return null!;
    }


    public IEnumerable<AuthEntity> GetAllAuths()
    {
        var authentications = _authRepository.GetAll();
        return authentications;
    }


    public AuthEntity UpdateAuth(AuthEntity authEntity)
    {
        var updateAuthEntity = _authRepository.Update(x => x.CustomerId==authEntity.CustomerId, authEntity);
        return updateAuthEntity;
    }

    public void DeleteAuth( Guid customerId)
    {
        _authRepository.Delete(x=> x.CustomerId==customerId);   
    }

}
