using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model;
using Model.Entities;
using Service.EntityService;
using Service.RoleService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace Service.RoleService
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        private readonly DbContext _context;
        private readonly DbSet<Role> _dbSet;
        public RoleService(ShareMoneyContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Role>();
        }

        public IPagedList<RoleDto> GetDataByPage(RoleSearchDto SearchModel)
        {
            var query = _dbSet.Select(x => new RoleDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            });

            if (SearchModel != null) 
            {
                if (!string.IsNullOrEmpty(SearchModel.Code))
                {
                    query = query.Where(x => x.Code.Contains(SearchModel.Code));
                }
                if (!string.IsNullOrEmpty(SearchModel.Name))
                {
                    query = query.Where(x => x.Name.Contains(SearchModel.Name));
                }
            }

            query = query.OrderBy(x => x.Id);
            return query.ToPagedList(SearchModel.PageIndex, SearchModel.PageSize);
        }
    }
}
