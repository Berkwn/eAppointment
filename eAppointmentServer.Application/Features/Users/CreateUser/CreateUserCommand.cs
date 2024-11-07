using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string firstName,
        string lastName,
        string email,
        string userName,
        string password,
        List<Guid> RoleIds) : IRequest<Result<string>>
    {
    }
}
