using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Infranstructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace eAppointmentServer.Infranstructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfranstructure(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddIdentity<AppUser, AppRole>(action =>
            {
                action.Password.RequiredLength = 1;
                action.Password.RequireUppercase = false;
                action.Password.RequireLowercase = false;
               
            }).AddEntityFrameworkStores<ApplicationDbContext>();

           
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            services.Scan(Action =>
            {
                Action.FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime();

                

            });

            return services;
        }
    }
}
