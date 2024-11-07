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

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments
{
    public sealed class GetAllAppointmentsByDoctorIdQueryHandler(IAppointmentRepository appointmentRepository) : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
{
        public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository.Where(x => x.DoctorId == request.DoctorId).Include(x=>x.Patient).ToListAsync(cancellationToken);

            List<GetAllAppointmentsByDoctorIdQueryResponse> response = appointments.Select(x => new GetAllAppointmentsByDoctorIdQueryResponse(x.Id,x.StartDate, x.EndDate, x.Patient!.FirstName, x.Patient)).ToList();

            return response;
        }
    }
}
