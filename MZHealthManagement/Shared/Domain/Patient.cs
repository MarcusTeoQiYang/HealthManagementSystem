using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Patient : BaseDomainModel
    {
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public float Weight { get; set; }
        public int DepartmentId { get; set; }
    }

}
