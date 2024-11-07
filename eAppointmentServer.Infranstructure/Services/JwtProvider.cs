using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eAppointmentServer.Infranstructure.Services
{
    public sealed class JwtProvider(
        IConfiguration configuration,
        RoleManager<AppRole> roleManager,
        IUserRoleRepository userRoleRepository) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            List<AppUserRole> appRoles = await userRoleRepository.Where(x=>x.UserId==user.Id).ToListAsync();
            List<AppRole> roles = new();

            foreach (var userRole in appRoles)
            {
                AppRole role = await roleManager.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefaultAsync();

                if (role is not null) 
                {
                
                    roles.Add(role);    
                }
            }

            List<string> stringRoles= roles.Select(x=>x.Name).ToList();
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email), 
                new Claim(ClaimTypes.Role,JsonSerializer.Serialize(stringRoles))

            };
            DateTime expires = DateTime.Now.AddDays(1);

            SymmetricSecurityKey securityKey =
                new(Encoding.UTF8.GetBytes
                (configuration.GetSection("Jwt:SecretKey").Value ?? ""));
            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha512);

           

           JwtSecurityToken SecurityToken = new(
               issuer: configuration.GetSection("Jwt:Issuer").Value,// uygulama kim tarafından oluşturuldu
               audience:configuration.GetSection("Jwt:Audience").Value,// uygulamayı kim kullanacak
               claims:claims, // jwt içine değerler gönderilecek username password vs 
               notBefore: DateTime.Now,
               expires: expires, // ne kadar süre bu token devam edecek
               signingCredentials:signingCredentials // uygulamanın şifreleme türünü buradan ayarlıyoruz.

               );

            JwtSecurityTokenHandler handler = new();

            string token = handler.WriteToken(SecurityToken);
            return token;
        }
    }
}
