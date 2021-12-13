namespace Ecommerce_UserManagment.Controllers
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