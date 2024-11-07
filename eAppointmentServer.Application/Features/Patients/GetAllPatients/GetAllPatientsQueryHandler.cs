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

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients
{
    public sealed class GetAllPatientsQueryHandler(IPatientRepository patientRepository) : IRequestHandler<GetAllPatientsQuery, Result<List<Patient>>>
    {
        public async Task<Result<List<Patient>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
           List<Patient> patients= await patientRepository.GetAll().OrderBy(x=>x.FirstName).ThenBy(x=>x.LastName).ToListAsync(cancellationToken);

            return patients;
        }
    }
}
