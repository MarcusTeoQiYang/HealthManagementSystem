using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MZHealthManagement.Shared.Domain
{
    public class Appointment : BaseDomainModel, IValidatableObject
    {
        public DateTime? DateIn { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        [Required]
        public string Location { get; set; }
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();

            if (DateIn != null)
            {
                if (DateIn <= DateOut)
                {
                    yield return new ValidationResult("DateIn must be greater than DateOut", new[] { "DateIn" });
                }
            }
        }
    }
}
