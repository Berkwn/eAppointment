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

namespace eAppointmentServer.Application.Features.Patients.CreatePatients
{
    public sealed class CreatePatientsCommandHandler(IPatientRepository patientRepository ,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<CreatePatientsCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientsCommand request, CancellationToken cancellationToken)
        {
            if (patientRepository.Any(x => x.IdentityNumber == request.IdentityNumber)) 
            {

                return Result<string>.Failure("Patient Already recorded");
            }



            Patient patient= mapper.Map<Patient>(request);
           await patientRepository.AddAsync(patient);
           await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Hasta Başarıyla eklendi";



           
        }
    }
}
