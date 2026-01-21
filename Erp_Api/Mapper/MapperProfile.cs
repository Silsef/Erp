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
using Shared_Erp.Entite;
using Shared_Erp.Projet;
using Shared_Erp.Tache;

namespace Erp_Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employe, EmployeDTO>()
                .ForMember(d => d.EmployeEntites, opt=>opt.MapFrom(src=>src.EmployeEntites))
                .ReverseMap();

            CreateMap<EmployeEntite, EmployeEntiteDTO>()
                .ForMember(dest => dest.NomEntreprise, opt => opt.MapFrom(src => src.Entite.Nom))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Entite.Telephone))
                .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.Entite.Adresse));
            CreateMap<Adresse, AdresseDTO>().ReverseMap();

            CreateMap<Entite, EntiteDTO>().ReverseMap();

            // Offre mappings
            CreateMap<Offre, OffreDTO>()
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

            CreateMap<Entite, EntiteDTO>().ReverseMap();
            CreateMap<Entite, EntiteCreateDTO>().ReverseMap();
            CreateMap<Entite, EntiteUpdateDTO>().ReverseMap();

            CreateMap<Projet, ProjetDTO>().ReverseMap();
            CreateMap<Projet, ProjetCreateDTO>().ReverseMap();
            CreateMap<Projet, ProjetUpdateDTO>().ReverseMap();

            CreateMap<Tache, TacheDTO>()
                .ForMember(d => d.ProjetId, opt => opt.MapFrom(src => src.Projet))
                .ForMember(d => d.EmployeAssigne, opt => opt.MapFrom(src => src.EmployeAssigne))
                .ReverseMap();
        }
    }
}