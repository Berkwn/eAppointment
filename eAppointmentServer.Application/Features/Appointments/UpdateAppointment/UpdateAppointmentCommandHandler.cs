using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandHandler(IMapper mapper,IUnitOfWork unitOfWork,IAppointmentRepository appointmentRepository) : IRequestHandler<UpdateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            DateTime startDate = Convert.ToDateTime(request.startDate);
            DateTime endDate=Convert.ToDateTime(request.endDate);

            Appointment appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.id, cancellationToken);
            if (appointment == null) {

                return Result<string>.Failure("Appointment bulunamadı");
            }

            bool isAppointmentDateNotAvailable = await appointmentRepository.AnyAsync(x => x.DoctorId == appointment.DoctorId &&
            ((x.StartDate < endDate && x.StartDate >= startDate) ||
            (x.EndDate > startDate && x.EndDate <= endDate) ||
            (x.StartDate >= startDate && x.EndDate <= endDate) ||
            (x.StartDate <= startDate && x.EndDate >= endDate)
            ),cancellationToken);


            if (isAppointmentDateNotAvailable)
            {
                return Result<string>.Failure("Aynı zaman aralıklarında randevu olamaz");
            }
            appointment.StartDate=Convert.ToDateTime(request.startDate);
            appointment.EndDate=Convert.ToDateTime(request.endDate);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Randevu başarıyla güncellendi";

        }
    }
}
