using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Role
    {
        bool SaveChanges();

        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int Id);
        Task CreateRoleAsync(Role rol);
        void UpdateRole(Role rol);
        void DeleteRole(Role rol);
    }
}