using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_UserManagment.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string AdditionalInformation;
    }
}
