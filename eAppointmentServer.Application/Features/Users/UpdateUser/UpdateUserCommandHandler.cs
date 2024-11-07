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

namespace eAppointmentServer.Application.Features.Users.UpdateUser
{
    public sealed class UpdateUserCommandHandler(UserManager<AppUser> userManager,IUserRoleRepository userRoleRepository,
        IUnitOfWork unitOfWork ,IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await userManager.FindByIdAsync(request.id.ToString());
            if (user == null) 
            {
                return Result<string>.Failure("user is not found");
            }
            if (user.Email != request.email)
            {
                if (await userManager.Users.AnyAsync(x => x.Email == request.email))
                {
                    return Result<string>.Failure("email zaten kayıtlı");
                }

            }

            if (user.UserName != request.userName) 
            {
                if (await userManager.Users.AnyAsync(x => x.UserName == request.userName))
                {
                    return Result<string>.Failure("username zaten kayıtlı");
                }
            }

            

            mapper.Map(request,user);
         

            IdentityResult result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) 
            {
                return Result<string>.Failure(result.Errors.Select(x => x.Description).ToList());
            }

            if (request.RoleIds.Any())
            {
                List<AppUserRole> userRoles = await userRoleRepository.Where(x=>x.UserId==request.id).ToListAsync();
                userRoleRepository.DeleteRange(userRoles);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                foreach (var roleId in request.RoleIds)
                {
                    AppUserRole userRole = new()
                    {
                        RoleId = roleId,
                        UserId = request.id
                    };

                    userRoles.Add(userRole);
                }

                await userRoleRepository.AddRangeAsync(userRoles,cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "user update is succesfull";
        }
    }
}
