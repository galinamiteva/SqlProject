

using Infrastructure.Dtos;
using Infrastructure.Entitites;
using Infrastructure.Repositories;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class RoleService(RoleRepository roleRepository)
{
    private readonly RoleRepository _roleRepository = roleRepository;



    //Create
    public async Task<RoleDto> CreateRoleAsync(string roleName)
    {
        try
        {
            var result = await _roleRepository.GetOneAsync(x => x.RoleName == roleName);
            result ??= await _roleRepository.CreateAsync(new RoleEntity { RoleName = roleName });

            return new RoleDto { Id = result.Id, RoleName = result.RoleName };
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }


    public RoleEntity CreateRole(string roleName)
    {
        var result = _roleRepository.GetOne(x => x.RoleName == roleName);
        result ??= _roleRepository.Create(new RoleEntity() { Id = result!.Id, RoleName = result.RoleName });

        return new RoleEntity { Id = result.Id, RoleName = result.RoleName };
    }


    public async Task<RoleDto> GetRoleAsync(Expression<Func<RoleEntity, bool>> predicate)
    {
        try
        {
            var result = await _roleRepository.GetOneAsync(predicate);
            if (result != null)
                return new RoleDto { Id = result.Id, RoleName = result.RoleName };
        }
        catch { }
        return null!;
    }

    public RoleEntity GetRoleByName(RoleDto role)
    {
        var roleEntity = _roleRepository.GetOne(x => x.RoleName == role.RoleName);
        return roleEntity;
    }

    public RoleEntity GetRoleById(int Id)
    {
        var roleEntity = _roleRepository.GetOne(x => x.Id == Id);
        return roleEntity;
    }


    public IEnumerable<RoleDto> GetAllRoles()
    {
        var roles = new List<RoleDto>();
        try
        {
            var result = _roleRepository.GetAll();

            if (result != null)
            {
                foreach (var role in result)
                    roles.Add(new RoleDto
                    {
                        Id = role.Id,
                        RoleName = role.RoleName
                    });
            }
            return roles;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;


    }

    public IEnumerable<RoleEntity> GetRoles()
    {
        var roles = new List<RoleEntity>();
        return roles;
    }


    public async Task<RoleDto> UpdateRoleAsync(RoleDto updatedRole)
    {
        try
        {
            var entity = await _roleRepository.GetOneAsync(x => x.Id == updatedRole.Id);
            if (entity != null)
            {
                entity.RoleName = updatedRole.RoleName!;

                var result = await _roleRepository.UpdateAsync(x => x.Id == updatedRole.Id, entity);
                if (result != null)
                    return new RoleDto { Id = updatedRole.Id, RoleName = updatedRole.RoleName };
            }
        }
        catch { }
        return null!;
    }


    public RoleEntity UpdateRole(RoleEntity roleEntity)
    {
        var updatedRoleEntity = _roleRepository.Update(x => x.Id == roleEntity.Id, roleEntity);
        return updatedRoleEntity;
    }

    public void DeleteRole(RoleDto role)
    {
        _roleRepository.Delete(x => x.RoleName == role.RoleName);
    }

}
