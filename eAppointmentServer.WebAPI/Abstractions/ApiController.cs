using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointmentServer.WebAPI.BaseController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public abstract class ApiController : ControllerBase
    {
        public readonly IMediator _mediator;

       
        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
