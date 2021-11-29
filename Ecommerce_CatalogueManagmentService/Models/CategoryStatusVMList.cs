using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class CategoryStatusVMList
    {
        public Guid CSId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
    }
}
