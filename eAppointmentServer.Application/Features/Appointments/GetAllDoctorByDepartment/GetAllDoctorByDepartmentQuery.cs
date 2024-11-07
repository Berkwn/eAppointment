using eAppointmentServer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorByDepartment
{
    public sealed record GetAllDoctorByDepartmentQuery(int departmentValue) :IRequest<Result<List<Doctor>>>
    {
    }
}
