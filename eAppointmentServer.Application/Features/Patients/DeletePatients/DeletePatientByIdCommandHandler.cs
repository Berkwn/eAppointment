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

namespace eAppointmentServer.Application.Features.Patients.DeletePatients
{
    public sealed class DeletePatientByIdCommandHandler(IPatientRepository patientRepository,IUnitOfWork unitOfWork) : IRequestHandler<DeletePatientByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
           Patient patient = await patientRepository.GetByExpressionAsync(x=>x.Id==request.id,cancellationToken);
            if (patient == null) 
            {
                return Result<string>.Failure("böyle bir hasta bulunamadı");
            }

              patientRepository.Delete(patient);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Hasta başarıyla silindi";
             

        }
    }
}
