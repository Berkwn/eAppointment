using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid id,
         string firstName,
        string lastName,
        string email,
        string userName,
        List<Guid> RoleIds) :IRequest<Result<string>>
    {
    }
}
