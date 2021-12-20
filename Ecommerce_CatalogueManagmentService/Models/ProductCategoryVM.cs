using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class ProductCategoryVM
    {
        public Guid ProductCategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
