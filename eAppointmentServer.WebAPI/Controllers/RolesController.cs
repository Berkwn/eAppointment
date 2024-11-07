using eAppointmentServer.Application.Features.Roles.RoleSync;
using eAppointmentServer.WebAPI.BaseController;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RolesController : ApiController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Sync(RoleSyncCommand request,CancellationToken cancellationToken)
        {
            var response= await _mediator.Send(request,cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

    }
}
