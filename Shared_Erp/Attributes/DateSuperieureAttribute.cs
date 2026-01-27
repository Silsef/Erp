using System.ComponentModel.DataAnnotations;

namespace Shared_Erp.Attributes // Ou votre namespace habituel
{
    public class DateSuperieureAttribute : ValidationAttribute
    {
        private readonly string _dateDebutPropertyName;

        public DateSuperieureAttribute(string dateDebutPropertyName)
        {
            _dateDebutPropertyName = dateDebutPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateFin = value as DateTime?;

            var propertyInfo = validationContext.ObjectType.GetProperty(_dateDebutPropertyName);

            if (propertyInfo == null)
                return new ValidationResult($"Propriété {_dateDebutPropertyName} introuvable");

            var dateDebut = propertyInfo.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (dateFin.HasValue && dateDebut.HasValue && dateFin < dateDebut)
            {
                return new ValidationResult(ErrorMessage ?? "La date de fin doit être après la date de début.");
            }

            return ValidationResult.Success;
        }
    }
}