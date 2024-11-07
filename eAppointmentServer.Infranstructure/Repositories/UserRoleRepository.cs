using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.Infranstructure.Context;
using GenericRepository;

namespace eAppointmentServer.Infranstructure.Repositories;

public sealed class UserRoleRepository : Repository<AppUserRole, ApplicationDbContext>,
    IUserRoleRepository
{
    public UserRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
