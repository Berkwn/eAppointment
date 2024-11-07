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

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorByDepartment
{
    public sealed class GetAllDoctorByDepartmentQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorByDepartmentQuery, Result<List<Doctor>>>
    {
        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorByDepartmentQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository.
                Where(x => x.department == request.departmentValue).
                OrderBy(x => x.FirstName).
                ToListAsync();

            return doctors;
        }
    }
}
