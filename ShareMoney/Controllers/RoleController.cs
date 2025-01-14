using Azure;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Newtonsoft.Json;
using Service.RoleService;
using Service.RoleService.Dto;
using Web.Models.Common;
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

            } else
            {
                searchModel.PageIndex = PageIndex;
                searchModel.PageSize = PageSize;
            }
            HttpContext.Session.SetString("SearchModel", JsonConvert.SerializeObject(searchModel));

            var data = _roleService.GetDataByPage(searchModel);
            return PartialView("_ViewData", data);
        }

        public IActionResult Create()
        {
            var createModal = new CreateVM();
            return View(createModal);
        }

        [HttpPost]
        public ResponseData Create(CreateVM model)
        {
            var result = new ResponseData() { status = true, message = "Thêm mới thành công" };
            try
            {
                if (ModelState.IsValid)
                {
                    Role role = new Role()
                    {
                        Code = model.Code,
                        Name = model.Name
                    };
                    _roleService.Create(role);
                } else
                {
                    result.ErrorMessage("Thêm mới thất bại");
                    return result;
                }
            }
            catch (Exception)
            {
                result.ErrorMessage("Thêm mới thất bại");
                return result;
            }

            return result;
        }
    }
}
