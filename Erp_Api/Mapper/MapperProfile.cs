using AutoMapper;
using Erp_Api;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Entity.Tables.Jointures;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp;
using Shared_Erp.Employe;

namespace Erp_Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employe, EmployeDTO>()
                .ForMember(d => d.EmployeEntreprises, opt=>opt.MapFrom(src=>src.EmployeEntreprises))
                .ReverseMap();

            CreateMap<EmployeEntreprise, EmployeEntrepriseDTO>()
                .ForMember(dest => dest.NomEntreprise, opt => opt.MapFrom(src => src.Entreprise.Nom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Entreprise.Telephone))
                .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Entreprise.Adresse));
            CreateMap<Adresse, AdresseDTO>().ReverseMap();

            CreateMap<Entreprise, EntrepriseDTO>().ReverseMap();
        }
    }
}