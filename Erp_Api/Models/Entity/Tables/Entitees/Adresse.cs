using Erp_Api.Models.Entity.Tables.Entitees;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_e_adresse_adr")]
public class Adresse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("adr_id")]
    public int Id { get; set; }

    [Column("adr_rue")]
    public string Rue { get; set; } = null!;

    [Column("adr_ville")]
    public string Ville { get; set; } = null!;

    [Column("adr_code_postal")]
    public string CodePostal { get; set; } = null!;

    [Column("adr_pays")]
    public string Pays { get; set; } = null!;

    // FK vers Employe
    public int? EmployeId { get; set; }
    public Employe? Employe { get; set; }

    // FK vers Entreprise
    public int? EntrepriseId { get; set; }
    public Entite? Entite { get; set; }
}