using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_UserManagment.Identity.Entities
{
    public class TblEmployee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
    }
}
