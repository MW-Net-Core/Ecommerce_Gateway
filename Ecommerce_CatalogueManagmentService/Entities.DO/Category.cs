using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_CatalogueManagmentService.Entities.DO
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public Status _status { get; set; }
    }
}
