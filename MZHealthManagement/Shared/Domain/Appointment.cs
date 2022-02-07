using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Appointment : BaseDomainModel
    {
        public string Location { get; set; }
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
