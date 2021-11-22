using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Models
{
    public class StatusCategoryVM
    {
        public Guid CsId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid StatusId { get; set; }
    }
}
