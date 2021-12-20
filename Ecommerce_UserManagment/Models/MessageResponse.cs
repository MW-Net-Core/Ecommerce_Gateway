using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_UserManagment.Models
{
    public class MessageResponse<T>
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

        /// <summary>
        /// 
        /// </summary>
        public ErrorMessage ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; set; }
    }
}
