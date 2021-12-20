using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Entities.DO
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public static implicit operator bool(Product v)
        {
            throw new NotImplementedException();
        }
        public virtual ProductStatus ProductStatus { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}