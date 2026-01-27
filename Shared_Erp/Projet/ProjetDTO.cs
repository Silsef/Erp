using Shared_Erp.Enums;
using Shared_Erp.Employe;
using Shared_Erp.Entite;
using Shared_Erp.Tache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared_Erp.TypeProjet;

namespace Shared_Erp.Projet
{
    public class ProjetDTO
    {
        public int Id { get; set; }

        public string Nom { get; set; }
        public string? Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string? EntiteRealisatriceNom { get; set; }
        public string? EntiteClienteNom { get; set; }
        public NiveauPriorite Priorite { get; set; }
        public Statut Statut { get; set; }
        public string? TypeProjetNom { get; set; }
        public int? TypeProjetId { get; set; }
        public List<TacheDTO> Taches { get; set; } = new List<TacheDTO>();
        public string? EmployeResponsablePrenom { get; set; }

        public StatutDelai EtatDelai
        {
            get
            {
                if (Statut == Statut.Termine || Statut == Statut.Archivé)
                {
                    return StatutDelai.Dans_Les_Temps;
                }

                if (!DateFin.HasValue)
                {
                    return StatutDelai.Dans_Les_Temps;
                }

                var tempsRestant = DateFin.Value - DateTime.Now;

                if (tempsRestant.TotalSeconds < 0)
                {
                    return StatutDelai.En_Retard;
                }

                if (tempsRestant.TotalDays <= 30)
                {
                    return StatutDelai.Imminent;
                }

                return StatutDelai.Dans_Les_Temps;
            }
        }
    }
    }
