using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Prescription : BaseDomainModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int DiagnosticID { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
    }
}
