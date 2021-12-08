using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Entities.DO
{
    public class ProductStatus
    {
        [Key]
        public Guid PsId { get; set; }


        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }


        public Guid StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public virtual Status Status { get; set; }


    }
}