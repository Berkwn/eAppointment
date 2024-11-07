using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatients
{
    public sealed record CreatePatientsCommand
        (string FirstName
        ,string LastName
        ,string City,
        string Town,
        string FullAddress,
        string IdentityNumber
        ) :IRequest<Result<string>>
    {
    }
}
