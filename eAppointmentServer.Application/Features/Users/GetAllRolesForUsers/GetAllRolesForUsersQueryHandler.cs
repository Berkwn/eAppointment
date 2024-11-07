using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllRolesForUsers
{
    public sealed class GetAllRolesForUsersQueryHandler(RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRolesForUsersQuery, Result<List<AppRole>>>
    {
        public async Task<Result<List<AppRole>>> Handle(GetAllRolesForUsersQuery request, CancellationToken cancellationToken)
        {
            List<AppRole> role = await roleManager.Roles.OrderBy(x=>x.Name).ToListAsync(cancellationToken);
            return role;

        }
    }
}
