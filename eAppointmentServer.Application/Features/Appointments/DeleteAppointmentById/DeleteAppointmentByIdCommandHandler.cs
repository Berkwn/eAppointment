using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById
{
    public sealed class DeleteAppointmentByIdCommandHandler(IAppointmentRepository appointmentRepository,IUnitOfWork unitOfWork) : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(x=>x.Id==request.Id);
            if (appointment == null) 
            {
                return Result<string>.Failure("Böyle bir randevu bulunamadı");
            }
            if (appointment.IsComplated)
            {
                return Result<string>.Failure("Tamamanmış randevuları silemezsiniz");
            }

             appointmentRepository.Delete(appointment);
           await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Randevu başarıyla silindi";
        }
    }
}
