using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogManagmentService.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }


        [StringLength(200), Required]   //add unique also
        public string CategoryName { get; set; }

        [StringLength(300)]
        public string CategoryDescription { get; set; }
    }
}
