using System.ComponentModel.DataAnnotations;

namespace MyFinances.Models
{
    public class AddModalViewModel : IValidatableObject
    {
        public DateTime HiddenDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Kwota musi być większa od 0.")]
        public double? Amount { get; set; }

        public int? WorkedHours { get; set; }
        public double? PayPerHour { get; set; }
        public int TransactionType = 1;
        public int Category { get; set; }

        public List<Categories>? ExpenseCategories { get; set; }
        public List<Categories>? IncomeCategories { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (Category == 4)  // Jeśli wybrana kategoria to Wynagrodzenie (id=4)
            {
                if (!WorkedHours.HasValue || WorkedHours.Value <=0)
                {
                    errors.Add(new ValidationResult("Przepracowane godziny są wymagane.", new[] { nameof(WorkedHours) }));
                }else if (WorkedHours.Value > 24)
                {
                    errors.Add(new ValidationResult("Przepracowane godziny nie mogą przekraczać 24 godzin!", new[] { nameof(WorkedHours) }));
                }
                if (!PayPerHour.HasValue || PayPerHour.Value <= 0)
                {
                    errors.Add(new ValidationResult("Płaca za godzinę jest wymagana.", new[] { nameof(PayPerHour) }));
                }
            }
            else
            {
                if (!Amount.HasValue || Amount.Value <= 0)
                {
                    errors.Add(new ValidationResult("Kwota jest wymagana.", new[] { nameof(Amount) }));
                }
            }

            return errors;
        }
    }
}