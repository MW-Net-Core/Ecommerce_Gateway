using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class ProductStatusVMList
    {
        public Guid PsId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    }
}
