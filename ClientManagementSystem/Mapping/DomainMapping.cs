using AutoMapper;
using ClientManagementSystem.Common.Dto;
using ClientManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagementSystem.Api.Mapping
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<Client, ClientDto>();
        }
    }
}
