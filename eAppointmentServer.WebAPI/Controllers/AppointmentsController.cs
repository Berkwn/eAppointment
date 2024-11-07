﻿using eAppointmentServer.Application.Features.Appointments.CreateAppointment;
using eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById;
using eAppointmentServer.Application.Features.Appointments.GetAllAppointments;
using eAppointmentServer.Application.Features.Appointments.GetAllDoctorByDepartment;
using eAppointmentServer.Application.Features.Appointments.GetPatientByIdentityNumber;
using eAppointmentServer.Application.Features.Appointments.UpdateAppointment;
using eAppointmentServer.WebAPI.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentsController : ApiController
    {
        public AppointmentsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetDoctorsByDepartment(GetAllDoctorByDepartmentQuery request,CancellationToken cancellationToken)
        {
            var response =await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
            
        }

        [HttpPost]
        public async Task<IActionResult> GetAllByDoctorId(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        public async Task<IActionResult> GetPatientByIdentityNumber(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);

        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteAppointmentByIdCommand request,CancellationToken cancellationToken)
        {
            var response=await _mediator.Send(request,cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAppointmentCommand request,CancellationToken cancellationToken)
        {
            var response =await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}