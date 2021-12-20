using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Ecommerce_UserManagment.Models;

namespace Ecommerce_UserManagment.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly List<int> TargettedActionmethodEnumIds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Guid organizationId = GetRequestedOrganizationId(context);

            //in case of unauthorized 401 access then return msgobj declared below
            Type msgbase = typeof(MessageResponse<>);
            var msgresp = msgbase.MakeGenericType((((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).MethodInfo).ReturnType.GenericTypeArguments[0].GenericTypeArguments[0]);
            dynamic msgobj = Activator.CreateInstance(msgresp);
            msgobj.StatusCode = 401;

            //if (organizationId != Guid.Empty)
            //{
            //    var configurationService = context.HttpContext.RequestServices.GetService<IConfigurationService>();

            //    if (organizationId == configurationService.AllOrganizationKey)
            //    {

            //    }
            //    else
            //    {
            //        var userClaims = context.HttpContext.User.Claims;
            //        bool IsInternal = Convert.ToBoolean(userClaims.FirstOrDefault(x => x.Type == "isInternal").Value);
            //        Guid LoggedInUserId = Guid.Parse(userClaims.FirstOrDefault(x => x.Type == "userId").Value);

            //        if (IsInternal)
            //        {
            //            var strOrgEnumIds = userClaims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            //            var assignedOrganizationWithEnumIds = new List<int>();

            //            foreach (var role in strOrgEnumIds)
            //            {
            //                var UserExternalRolesEnums = JsonConvert.DeserializeObject<int>(role);
            //                assignedOrganizationWithEnumIds.Add(UserExternalRolesEnums);
            //            }

            //            bool isInternalAllOrganizationAssigned = Convert.ToBoolean(userClaims.FirstOrDefault(x => x.Type == "isInternalAllOrganizationAssigned").Value);

            //            if (isInternalAllOrganizationAssigned)
            //            {

            //                var allowed = assignedOrganizationWithEnumIds.Any(x => TargettedActionmethodEnumIds.Any(y => y == x));
            //                if (!allowed)
            //                {
            //                    context.Result = new ObjectResult(msgobj);
            //                }
            //            }
            //            else
            //            {
            //                //db hit based on requested organization id
            //                var dbContext = context.HttpContext.RequestServices.GetService<MSUserManagementContext>();
            //                var userHasOrganizationRights = dbContext.TblOrganizationInternalUsers.Any(x => x.UserId == LoggedInUserId && x.OrganizationId == organizationId);
            //                if (userHasOrganizationRights)
            //                {
            //                    var allowed = assignedOrganizationWithEnumIds.Any(x => TargettedActionmethodEnumIds.Any(y => y == x));
            //                    if (!allowed)
            //                    {
            //                        context.Result = new ObjectResult(msgobj);
            //                    }
            //                }
            //                else
            //                {
            //                    context.Result = new ObjectResult(msgobj);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            var strOrgEnumIds = userClaims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            //            var assignedOrganizationWithEnumIds = new List<ExternalRoleEnumsVM>();

            //            foreach (var role in strOrgEnumIds)
            //            {
            //                var UserExternalRolesEnums = JsonConvert.DeserializeObject<ExternalRoleEnumsVM>(role);
            //                assignedOrganizationWithEnumIds.Add(UserExternalRolesEnums);
            //            }

            //            if (assignedOrganizationWithEnumIds.Any(x => x.OrgId == organizationId))
            //            {
            //                var assignedOrganizationEnumIds = assignedOrganizationWithEnumIds.FirstOrDefault(x => x.OrgId == organizationId).EnumIds;

            //                var allowed = assignedOrganizationEnumIds.Any(x => TargettedActionmethodEnumIds.Any(y => y == x));
            //                if (!allowed)
            //                {
            //                    context.Result = new ObjectResult(msgobj);
            //                }
            //            }
            //            else
            //            {
            //                context.Result = new ObjectResult(msgobj);
            //            }
            //        }
            //    }
            //}
        }


        private Guid GetRequestedOrganizationId(AuthorizationFilterContext context)
        {
            if (Guid.TryParse(context.HttpContext.Request.Headers["CurrentOrganizationId"], out Guid RequestedOrganizationId))
                return RequestedOrganizationId;
            else
                return Guid.Empty;
        }
    }
}
