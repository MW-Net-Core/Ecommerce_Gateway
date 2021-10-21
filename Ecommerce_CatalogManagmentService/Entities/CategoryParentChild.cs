using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogManagmentService.Entities
{
    public class CategoryParentChild
    {
        [Key]
        public Guid CPCId { get; set; }

        
        public Guid CategoryParentId { get; set; }
        public Guid CategoryChildId { get; set; }



        [ForeignKey(nameof(CategoryParentId))]
        public ICollection<Category> _categories { get; set; }

    }
}
