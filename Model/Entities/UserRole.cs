using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    [Table("UserRole")]
    public class UserRole : Audit
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
