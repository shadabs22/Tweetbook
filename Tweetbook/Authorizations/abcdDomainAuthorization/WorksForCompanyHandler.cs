using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tweetbook.Authorizations.abcdDomainAuthorization
{
    public class WorksForCompanyHandler : AuthorizationHandler<WorksForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorksForCompanyRequirement requirement)
        {
            var userEmailAddress = context.User?.FindFirst(ClaimTypes.Email).Value ?? string.Empty;
            if (userEmailAddress.Contains(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
