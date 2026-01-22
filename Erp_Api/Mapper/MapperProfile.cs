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
using Shared_Erp.FeuilleTemps;

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
            CreateMap<Offre, OffreDTO>();
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

            CreateMap<Projet, ProjetDTO>()
                .ForMember(d=> d.EmployeResponsablePrenom, opt => opt.MapFrom(src => src.EmployeResponsable.Prenom))
                .ForMember(d=> d.EntiteClienteNom, opt => opt.MapFrom(src => src.EntiteRealisatrice.Nom))
                .ForMember(d=> d.EntiteClienteNom, opt => opt.MapFrom(src => src.EntiteCliente.Nom))
                .ReverseMap();
            CreateMap<Projet, ProjetCreateDTO>().ReverseMap();
            CreateMap<Projet, ProjetUpdateDTO>().ReverseMap();

            CreateMap<Tache, TacheDTO>()
            .ForMember(d => d.EmployeAssigneId, opt => opt.MapFrom(src => src.EmployeAssigneId))
            .ForMember(d => d.ProjetId, opt => opt.MapFrom(src => src.ProjetId))
            .ForMember(d => d.TacheParenteId, opt => opt.MapFrom(src => src.TacheParenteId))
            .ForMember(d => d.ProjetNom, opt => opt.MapFrom(src => src.Projet != null ? src.Projet.Nom : null))
            .ForMember(d => d.EmployeAssigneNom, opt => opt.MapFrom(src => src.EmployeAssigne != null ? $"{src.EmployeAssigne.Prenom} {src.EmployeAssigne.Nom}" : null))
            .ReverseMap();

            CreateMap<TacheCreateDTO, Tache>();
            CreateMap<TacheUpdateDTO, Tache>();

            CreateMap<FeuilleTemps,FeuilleTempsDTO>()
                .ForMember(d => d.EmployeNom, opt => opt.MapFrom(src => src.Employe != null ? $"{src.Employe.Nom}" : null))
                .ForMember(d => d.ProjetNom, opt => opt.MapFrom(src => src.Projet != null ? src.Projet.Nom : null))

                .ReverseMap();
            CreateMap<FeuilleTempsCreateDTO,FeuilleTemps>().ReverseMap();
            CreateMap<FeuilleTempsUpdateDTO,FeuilleTemps>().ReverseMap();

        }
    }
}