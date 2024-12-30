using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Role : Audit
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
