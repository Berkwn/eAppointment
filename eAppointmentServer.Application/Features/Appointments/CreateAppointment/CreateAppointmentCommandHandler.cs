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
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment
{
    public sealed class CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository,IPatientRepository patientRepository,IMapper mapper,IUnitOfWork unitOfWork) : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            DateTime startDate = Convert.ToDateTime(request.startDate);
            DateTime endDate = Convert.ToDateTime(request.endDate);

            Patient patient = new();
          if(request.PatientId == null)
          {
                patient = new Patient()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    City = request.City,
                    Town = request.Town,
                    FullAddress = request.FullAddress,
                    IdentityNumber = request.IdentityNumber,
                };
                await patientRepository.AddAsync(patient,cancellationToken);
        
          }

            bool isAppointmentDateNotAvailable = await appointmentRepository.AnyAsync(x => x.DoctorId == request.DoctorId &&
          ((x.StartDate < endDate && x.StartDate >= startDate) ||
          (x.EndDate > startDate && x.EndDate <= endDate) ||
          (x.StartDate >= startDate && x.EndDate <= endDate) ||
          (x.StartDate <= startDate && x.EndDate >= endDate)
          ), cancellationToken);


            if (isAppointmentDateNotAvailable)
            {
                return Result<string>.Failure("Aynı zaman aralıklarında randevu olamaz");
            }


            Appointment appointment = new()
            {
                StartDate =Convert.ToDateTime(request.startDate),
                EndDate = Convert.ToDateTime(request.endDate),
                PatientId = request.PatientId ?? patient.Id,
                DoctorId = request.DoctorId,
                IsComplated = false
            };

            await appointmentRepository.AddAsync(appointment, cancellationToken);
           await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Randevu başarıyla oluşturuldu";
        }
    }
}
