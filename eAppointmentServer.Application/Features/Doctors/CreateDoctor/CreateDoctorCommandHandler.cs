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

namespace eAppointmentServer.Application.Features.Doctors.CreateDoctor
{
    public sealed class CreateDoctorCommandHandler(IDoctorRepository doctorRepository,IMapper mapper,IUnitOfWork unitOfWork) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = mapper.Map<Doctor>(request);
            await doctorRepository.AddAsync(doctor);
            unitOfWork.SaveChangesAsync(cancellationToken);
            return "Doktor başarıyla oluşturuldu";
         
        }
    }
}
