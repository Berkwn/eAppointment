using eAppointmentServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eAppointmentServer.WebAPI
{
    public static class Helper
    {

        public static async Task CreateUserAsync(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var usermanager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                if (!usermanager.Users.Any())
                {
                    await usermanager.CreateAsync(new()
                    {
                        FirstName="berkan",
                        LastName="durmus",
                        Email="berkand@gmail.com",
                        UserName="admin",

                    }, "Admin123!");
                }

            }
        }
    }
}
