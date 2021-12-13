using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Gateway.Model
{
    public class LoginResponseVM
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isMfaCodeGenrateSuccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MFATokenEmail { get; set; }

    }
}
