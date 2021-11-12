using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;

namespace Shared.ModeloDeDominio
{
 
    public class Role : IdentityRole<int>
    {
        
    }
}
