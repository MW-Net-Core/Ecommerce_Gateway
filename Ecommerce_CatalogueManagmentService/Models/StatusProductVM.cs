using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class StatusProductVM
    {
        public Guid PsId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StatusId { get; set; }
    }
}
