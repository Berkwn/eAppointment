using AutoMapper;
using eAppointmentServer.Application.Features.Doctors.CreateDoctor;
using eAppointmentServer.Application.Features.Doctors.UpdateDoctor;
using eAppointmentServer.Application.Features.Patients.CreatePatients;
using eAppointmentServer.Application.Features.Patients.UpdatePatients;
using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.UpdateUser;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Application.MappingsProfile
{
    public sealed class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDoctorCommand, Doctor>().ForMember(member => member.department, options =>
            {
                options.MapFrom(map=>DepartmentEnum.FromValue(map.departmentvalue));
            });

            CreateMap<UpdateDoctorCommand, Doctor>().ForMember(member => member.department, options =>
            {

                options.MapFrom(map => DepartmentEnum.FromValue(map.departmentvalue));
            });

            CreateMap<CreatePatientsCommand, Patient>();

            CreateMap<UpdatePatientCommand, Patient>();

            CreateMap<CreateUserCommand, AppUser>();
            CreateMap<UpdateUserCommand, AppUser>();
        }
    }
}
