using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Diagnosis : BaseDomainModel
    {
        public int Cost { get; set; }
        public string Symptoms { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
