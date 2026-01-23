using System;
using System.Collections.Generic;
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
        public string Nom { get; set; }
    }
    public class TypeProjetUpdateDTO
    {
        public string Nom { get; set; }
    }
}