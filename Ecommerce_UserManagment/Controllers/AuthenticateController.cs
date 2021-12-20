using Ecommerce_UserManagment.Identity;
using Ecommerce_UserManagment.Identity.Email;
using Ecommerce_UserManagment.Identity.Entities;
using Ecommerce_UserManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ecommerce_UserManagment.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;

        //Initiallizer
        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.context = context;
        }

        //Action method for getting the token
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = new LoginResponseVM();

                result.access_token = GenerateTokenAsync(user.Id, user.UserName, user.Email, user.Email, _configuration["AppSettings:JWT_SECRET"].ToString(), 3600, user).Result;


                return Ok(result);
            }
            return Unauthorized();
        }


        private async Task<string> GenerateTokenAsync(
           string userId,
           string userName,
           string Name,
           string Email,
           string jwtSecret,
           int jwtExpiry, ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var userRoles = await userManager.GetRolesAsync(user);
            var key = Encoding.ASCII.GetBytes(jwtSecret);



            var identity = new ClaimsIdentity(authenticationType: "JWT");
            identity.AddClaim(new Claim(ClaimTypes.Email, Email));
            identity.AddClaim(new Claim("userId", userId));

            foreach (var userRole in userRoles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                identity
                ),
                Expires = DateTime.UtcNow.AddMinutes(jwtExpiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = tokenHandler.WriteToken(token);
            if (ValidateToken(tokenResult, jwtSecret))
                return tokenResult;
            return "";

        }


        private static bool ValidateToken(string authToken,string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(key);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }
        private static TokenValidationParameters GetValidationParameters(string key)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
            };
        }
        //Action method for register a client type of user
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                AdditionalInformation = "This is additional information"

            };

            string user_id = user.Id; //   for email might not be null

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });


            //string originalCode = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            //string code = HttpUtility.UrlEncode(originalCode);
            //string confirmationLink = Url.Action("ConfirmEmail", "Authencticate",
            //                                      new { userId = user.Id, token = originalCode.ToString() }, Request.Scheme);
            //using (StreamWriter writer = new StreamWriter(@"C:\Users\pc\source\testingdata.txt"))
            //{
            //    writer.WriteLine(code);
            //}




            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }




            string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            string confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new
            {
                userId = user.Id,
                token = confirmationToken
            },
               Request.Scheme);


            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        //Action method for confirmation of mail by the user[kill this please]
        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            //var lockUserTask = userManager.SetLockoutEnabledAsync(user, false);

            var originalCode = HttpUtility.UrlDecode(token);
            var result = await userManager.ConfirmEmailAsync(user, originalCode);



            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "Email confirmed" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User email confirmation failed!" });
            }
        }

        //Action method for registeration of adminstrator
        [AllowAnonymous]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            string confirmationToken = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            string confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new
            {
                userId = user.Id,
                token = confirmationToken
            },
               Request.Scheme);


            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);








            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }




        //Action Method for adding the new role
        [Authorize("Admin")]
        [HttpPost]
        [Route("add-role")]
        public async Task<IActionResult> AddRole([FromBody] RoleModel roleModel)
        {
            Guid id = Guid.NewGuid();
            bool exists = await roleManager.RoleExistsAsync(roleModel.RoleName);
            if (!exists)
            {
                var role = new IdentityRole
                {
                    Id = id.ToString(),
                    Name = roleModel.RoleName,
                    NormalizedName = roleModel.RoleNormaizedName
                };
                IdentityResult rs = await roleManager.CreateAsync(role);
                return Ok(new Response { Status = "Sucess", Message = "Added Sucessfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role already exists!" });
            }

        }

        //change password
        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> changePassword([FromBody] ResetPwdModel usermodel)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(usermodel.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, "3dsdsoft195*_Z");
                return Ok(new Response { Status = "Sucess", Message = "Password Edited Sucessfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Password not Edited Sucessfully" });
            }
        }

        //delete user
        [HttpPost]
        [Route("delete-user")]

        public async Task<ActionResult> DeleteUser(string userId)
        {
            if (userId == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Password not Edited Sucessfully" });
            }

            //get User Data from Userid
            var user = await userManager.FindByIdAsync(userId);
            var logins = await userManager.GetLoginsAsync(user);
            var rolesForUser = await userManager.GetRolesAsync(user);

            using (var transaction = context.Database.BeginTransaction())
            {
                IdentityResult result = IdentityResult.Success;
                foreach (var login in logins)
                {
                    result = await userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                }
                return Ok(new Response { Status = "Sucess", Message = "User deleted Sucessfully" });
            }
        }

        //delete role
        [HttpPost]
        [Route("delete-role")]
        public async Task<ActionResult> DeleteRole(string RoleName)
        {
            var role = await roleManager.FindByNameAsync(RoleName);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return Ok(new Response { Status = "Sucess", Message = "Role deleted Sucessfully" });
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role is not deleted Sucessfully" });
        }
    }
}