using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.Entite
{
    public class EntiteDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;

    }

    public class EntiteCreateDTO
    {
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;
    }

    public class EntiteUpdateDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public string? Telephone { get; set; }
        public bool EstEntreprise { get; set; } = true;
    }
}
