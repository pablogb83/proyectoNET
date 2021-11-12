﻿using DataAccessLayer.IDAL;
using Microsoft.AspNetCore.Identity;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Role_EF : IDAL_Role
    {
        private readonly WebAPIContext _context;

        private readonly RoleManager<Role> _roleManager;


        public DAL_Role_EF(WebAPIContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync(Role rol)
        {

            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }

            var result = await _roleManager.CreateAsync(rol);

        }

        public void DeleteRole(Role rol)
        {
            if (rol == null)
            {
                throw new ArgumentNullException(nameof(rol));
            }
            _context.Roles.Remove(rol);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int Id)
        {
            return _context.Roles.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateRole(Role rol)
        {
            //nothing
        }
    }
}