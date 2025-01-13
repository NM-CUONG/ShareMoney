using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RoleService.Dto
{
    public class RoleSearchDto : SearchBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
