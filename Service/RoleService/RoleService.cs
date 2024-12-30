using Model;
using Model.Entities;
using Service.EntityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RoleService
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        public RoleService(ShareMoneyContext context) : base(context)
        {

        }
    }
}
