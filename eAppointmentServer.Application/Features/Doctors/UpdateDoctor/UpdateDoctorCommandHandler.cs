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

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor
{
    internal sealed class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetByExpressionAsync(x => x.Id == request.id,cancellationToken);
            if (doctor is null)
            {
                return Result<string>.Failure("Böyle bir doktor bulunamadı");
            }

            mapper.Map(request, doctor);
            doctorRepository.Update(doctor);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doktor başaryıla güncellendi.";
        }
    }
}
