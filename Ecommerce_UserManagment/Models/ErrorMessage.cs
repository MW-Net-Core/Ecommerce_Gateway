using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_UserManagment.Models
{
    public class ErrorMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public int UniqueCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FriendlyMessage { get; set; }
    }
}
