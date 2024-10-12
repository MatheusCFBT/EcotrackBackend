using AutoMapper;
using EcotrackApi.ViewModels;
using EcotrackBusiness.Models;

namespace EcotrackApi.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Cliente, RegisterClienteViewModel>().ReverseMap();
        }
    }
}