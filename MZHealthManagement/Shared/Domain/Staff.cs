using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Staff : BaseDomainModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }

    }
}
