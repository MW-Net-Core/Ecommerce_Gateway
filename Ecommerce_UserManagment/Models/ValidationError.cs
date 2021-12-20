using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_UserManagment.Models
{
    public class ValidationError
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<string> FieldName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Error { get; set; }
    }
}
