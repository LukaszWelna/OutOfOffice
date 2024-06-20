using AutoMapper;
using OutOfOffice.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<CreateEmployeeDto, OutOfOffice.Domain.Entities.Employee>();
        }
    }
}
