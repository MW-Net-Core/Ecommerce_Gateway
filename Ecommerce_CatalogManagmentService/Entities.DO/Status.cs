using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogManagmentService.Entities.DO
{
    public class Status
    {
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusDescription { get; set; }
        public bool Associated { get; set; }
        public ICollection<Category> categories { get; set; }
    }
}
