using AutoMapper;
using Erp_Api;
using Erp_Api.Models.Entity.Tables.Entitees;
using Erp_Api.Models.Entity.Tables.Jointures;
using Microsoft.AspNetCore.Mvc;
using Shared_Erp.Adresse;
using Shared_Erp.Employe;
using Shared_Erp.Entretien;
using Shared_Erp.Offre;
using Shared_Erp.Candidature;
using Shared_Erp.Entreprise;

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

            // Offre mappings
            CreateMap<Offre, OffreDTO>()
                .ForMember(d => d.NomEntreprise, opt => opt.MapFrom(src => src.Entreprise != null ? src.Entreprise.Nom : null))
                .ForMember(d => d.TypeContratLibelle, opt => opt.MapFrom(src => src.TypeContrat.Libelle));
            CreateMap<OffreCreateDTO, Offre>();
            CreateMap<OffreUpdateDTO, Offre>();

            // Candidature mappings
            CreateMap<Candidature, CandidatureDTO>()
                .ForMember(d => d.OffreTitre, opt => opt.MapFrom(src => src.Offre.Titre));
            CreateMap<CandidatureCreateDTO, Candidature>();
            CreateMap<CandidatureUpdateDTO, Candidature>();

            // Entretien mappings
            CreateMap<Entretien, EntretienDTO>()
                .ForMember(d => d.NomCandidat, opt => opt.MapFrom(src => src.Candidature.Nom))
                .ForMember(d => d.PrenomCandidat, opt => opt.MapFrom(src => src.Candidature.Prenom))
                .ForMember(d => d.OffreId, opt => opt.MapFrom(src => src.Candidature.OffreEmploiId))
                .ForMember(d => d.OffreTitre, opt => opt.MapFrom(src => src.Candidature.Offre.Titre))
                .ForMember(d => d.Nominterviewer, opt => opt.MapFrom(src => src.Interviewer != null ? $"{src.Interviewer.Prenom} {src.Interviewer.Nom}" : null));
            CreateMap<EntretienCreateDTO, Entretien>();
            CreateMap<EntretienUpdateDTO, Entretien>();
        }
    }
}