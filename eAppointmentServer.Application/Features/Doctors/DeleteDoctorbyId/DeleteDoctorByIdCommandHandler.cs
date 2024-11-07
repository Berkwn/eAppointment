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

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorbyId
{
    public sealed class DeleteDoctorByIdCommandHandler(IDoctorRepository doctorRepository,IUnitOfWork unitOfWork) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = await doctorRepository.GetByExpressionAsync(x => x.Id == request.Id, cancellationToken);
            if (doctor is null)
            {
                return Result<string>.Failure("Böyle bir doktor mevcut değil");
            }
             doctorRepository.Delete(doctor);
            await unitOfWork.SaveChangesAsync();

            return "Doktor başarıla silindi"; 
        }
    }
}
