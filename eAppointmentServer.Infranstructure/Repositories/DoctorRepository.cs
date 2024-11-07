using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.Infranstructure.Context;
using GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Infranstructure.Repositories
{
    public sealed class DoctorRepository : Repository<Doctor, ApplicationDbContext>, IDoctorRepository
    {
     
    }
}
