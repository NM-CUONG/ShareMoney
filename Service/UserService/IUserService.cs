using Model.Entities;
using Service.EntityService;
using Service.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Service.UserService
{
    public interface IUserService : IEntityService<User>
    {
        IPagedList<UserDto> GetDataByPage(UserSearchDto SearchModel);
    }
}
