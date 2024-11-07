using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.UpdateAppointment
{
    public sealed record UpdateAppointmentCommand(
        Guid id,
        string startDate,
        string endDate) :IRequest<Result<string>>
    {
    }
}
