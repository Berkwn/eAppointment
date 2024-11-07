using eAppointmentServer.Application.Features.Patients.CreatePatients;
using eAppointmentServer.Application.Features.Patients.DeletePatients;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
using eAppointmentServer.Application.Features.Patients.UpdatePatients;
using eAppointmentServer.WebAPI.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientsController : ApiController
    {
        public PatientsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]

        public async Task<IActionResult> GetAll(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {

                var response= await _mediator.Send(request,cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientsCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePatientByIdCommand request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePatientCommand request,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode,response);
        }
    }
}
