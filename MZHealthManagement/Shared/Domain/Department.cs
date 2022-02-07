using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZHealthManagement.Shared.Domain
{
    public class Department : BaseDomainModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
