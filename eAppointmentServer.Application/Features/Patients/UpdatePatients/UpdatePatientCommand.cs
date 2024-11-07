using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatients
{
    public sealed record UpdatePatientCommand(Guid id,string firstName,string lastName,string City,string Town,string FullAddress):IRequest<Result<string>>
    {
    }
}
