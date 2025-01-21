using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;
using Service.EntityService;
using Service.UserService;
using Service.UserService.Dto;
using X.PagedList;
using X.PagedList.Extensions;

namespace Service.UserService
{
    public class UserService : EntityService<User>, IUserService
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserService(ShareMoneyContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public IPagedList<UserDto> GetDataByPage(UserSearchDto SearchModel)
        {
            var query = _dbSet.Select(x => new UserDto
            {
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                Email = x.Email,
                Fullname = x.Fullname,
                Phone = x.Phone,
                UserName = x.UserName,
            });

            if (SearchModel != null)
            {
               
            }

            query = query.OrderBy(x => x.Id);
            return query.ToPagedList(SearchModel.PageIndex, SearchModel.PageSize);
        }
    }
}
