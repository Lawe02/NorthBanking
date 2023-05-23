using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupaLibrary.ViewModels;
using WebbApp.Models;

namespace SupaLibrary.InfraStructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();
        }
    }
}
