using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.RoleService;
using Service.RoleService.Dto;
using Web.Models.Role;

namespace Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            var searchModel = new RoleSearchDto();
            var data = _roleService.GetDataByPage(searchModel);
            return View(data);
        }

        // Được gọi khi thao tác với pagination hoặc sau khi thao CRUD trên bảng dữ liệu
        public IActionResult GetData(int PageIndex, int PageSize)
        {
            var searchModelJson = HttpContext.Session.GetString("SearchModel");
            var searchModel = new RoleSearchDto();
            if (!String.IsNullOrEmpty(searchModelJson))
            {
                searchModel = JsonConvert.DeserializeObject<RoleSearchDto>(searchModelJson) ?? new RoleSearchDto();
            }

            searchModel.PageIndex = PageIndex;
            searchModel.PageSize = PageSize;

            var data = _roleService.GetDataByPage(searchModel);
            return PartialView("_ViewData", data);
        }

        public IActionResult Create()
        {
            var createModal = new CreateVM();
            return View(createModal);
        }

    }
}
