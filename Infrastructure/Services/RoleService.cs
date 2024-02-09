

using Infrastructure.Entitites;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class RoleService
{
    private readonly RoleRepository _roleRepository;

    public RoleService(RoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }


    public RoleEntity CreateRole (string roleName)
    {
        var roleEntity = _roleRepository.GetOne(x=> x.RoleName == roleName);
        roleEntity ??= _roleRepository.Create(new RoleEntity { RoleName = roleName }); 
        return roleEntity;
    }


    public RoleEntity GetOneRoleByRoleName (string roleName)
    {
        var roleEntity = _roleRepository.GetOne(x=> x.RoleName == roleName);
        return roleEntity;
    }

    public RoleEntity GetOneRoleByRoleId(int id)
    {
        var roleEntity = _roleRepository.GetOne(x => x.Id == id);
        return roleEntity;
    }

    public IEnumerable<RoleEntity> GetAllRoles() 
    { 
        var roles = _roleRepository.GetAll();
        return roles;
    }

    public RoleEntity UpdateRole(RoleEntity roleEntity)
    {
        var updatedRole = _roleRepository.Update(x => x.Id == roleEntity.Id, roleEntity);
        return updatedRole;
    }

    public bool DeleteRole(int id) 
    { 
        _roleRepository.Delete(x=> x.Id == id); 
        return true;
    }

}
