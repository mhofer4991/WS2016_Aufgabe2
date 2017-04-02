using BlogStorage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HoferBlog
{
    public class UserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, User resource)
        {
            var user = context.User;

            if (requirement.Name == Operations.Update.Name)
            {
                if (user.Identity.IsAuthenticated)
                {
                    var id = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (resource.Id.Equals(id))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
