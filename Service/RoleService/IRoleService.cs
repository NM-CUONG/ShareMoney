using Model.Entities;
using Service.EntityService;
using Service.RoleService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Service.RoleService
{
    public interface IRoleService : IEntityService<Role>
    {
        IPagedList<RoleDto> GetDataByPage(RoleSearchDto SearchModel);
    }
}
