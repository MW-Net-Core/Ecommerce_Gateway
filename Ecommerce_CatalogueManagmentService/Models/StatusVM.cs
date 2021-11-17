using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class StatusVM
    {
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatuDescription { get; set; }
    }
}
