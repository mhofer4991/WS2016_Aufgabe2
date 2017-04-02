using BlogStorage.Models;
using HoferBlog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HoferBlog
{
    public class BlogAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Blog>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Blog resource)
        {
            var user = context.User;

            if (requirement.Name == Operations.Create.Name)
            {
                if (user.Identity.IsAuthenticated)
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement.Name == Operations.Update.Name)
            {
                if (user.Identity.IsAuthenticated)
                {
                    var id = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (resource.UserID.Equals(id))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            else if (requirement.Name == Operations.Delete.Name)
            {
                if (user.Identity.IsAuthenticated)
                {
                    var id = user.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (resource.UserID.Equals(id))
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
