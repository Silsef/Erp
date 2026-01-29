using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Erp.TypeProjet
{
    public class TypeProjetDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }
    public class TypeProjetCreateDTO
    {
        [Required(ErrorMessage = "Le nom du type de projet est obligatoire")]
        public string Nom { get; set; }
    }
    public class TypeProjetUpdateDTO
    {
        public string Nom { get; set; }
    }
}