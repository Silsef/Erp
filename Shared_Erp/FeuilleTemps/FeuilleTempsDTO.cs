using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.FeuilleTemps
{
    public class FeuilleTempsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool EstMatin { get; set; }
        public int EmployeId { get; set; }
        public string EmployeNom { get; set; } = null!;
        public int? ProjetId { get; set; }
        public int? TacheId { get; set; }

        public string? ProjetNom { get; set; }
        public string? Commentaire { get; set; }

    }

    public class FeuilleTempsCreateDTO
    {
        public DateTime Date { get; set; }
        public bool EstMatin { get; set; }
        public int? ProjetId { get; set; }
        public int? TacheId { get; set; }

        public string? Commentaire { get; set; }
    }
    public class FeuilleTempsUpdateDTO
    {
        public int Id { get; set; }
        public string? Commentaire { get; set; }
        public int? TacheId { get; set; }
        public int? ProjetId { get; set; }


    }
}
