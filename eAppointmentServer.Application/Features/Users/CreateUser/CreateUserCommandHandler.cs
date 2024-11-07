using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser
{
    public sealed class CreateUserCommandHandler(UserManager<AppUser> userManager,IUserRoleRepository userRoleRepository,IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(x => x.Email == request.email)) 
            {
                return Result<string>.Failure("email already exist");
            }

            if(await userManager.Users.AnyAsync(x=>x.UserName== request.userName))
            {
                return Result<string>.Failure("username already exist");
            }

            AppUser user = mapper.Map<AppUser>(request);
            IdentityResult result = await userManager.CreateAsync(user,request.password);
            if (!result.Succeeded)
            {
               return Result<string>.Failure(result.Errors.Select(x=>x.Description).ToList());
            }
            List<AppUserRole> userRoles = new();
            foreach (var roleId in request.RoleIds)
            {
                AppUserRole userRole = new()
                {
                    RoleId = roleId,
                    UserId = user.Id
                };

                userRoles.Add(userRole);
            }

            await userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "user create is successfull";

        }
    }
}
