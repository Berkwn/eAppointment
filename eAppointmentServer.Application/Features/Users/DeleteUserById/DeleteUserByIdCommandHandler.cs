﻿using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.DeleteUserById
{
    public sealed class DeleteUserByIdCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<DeleteUserByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null) 
            {
                return Result<string>.Failure("böyle bir user bulunamadı");
            
            }

          IdentityResult result= await userManager.DeleteAsync(appUser);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(x => x.Description).ToList());
            }

            return "Delete user is successfull";



        }
    }
}
