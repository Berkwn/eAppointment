using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetPatientByIdentityNumber
{
    public sealed class GetPatientByIdentityNumberQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
    {
        public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
           Patient? patient= await patientRepository.GetByExpressionAsync(x=>x.IdentityNumber==request.IdentityNumber,cancellationToken);

            return patient;
        }
    }
}
