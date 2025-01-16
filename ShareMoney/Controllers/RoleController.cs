using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Model.Entities;
using Newtonsoft.Json;
using Service.RoleService;
using Service.RoleService.Dto;
using System.Security.Permissions;
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
            }

            searchModel.PageIndex = PageIndex;
            searchModel.PageSize = PageSize;

            HttpContext.Session.SetString("SearchModel", JsonConvert.SerializeObject(searchModel));

            var data = _roleService.GetDataByPage(searchModel);
            return PartialView("_ViewData", data);
        }


        public IActionResult SearchData(RoleSearchDto searchModel)
        {
            var search = new RoleSearchDto();
            if (searchModel != null)
            {
                search = searchModel;
            }
            var searchModelJson = HttpContext.Session.GetString("SearchModel");
            if (!String.IsNullOrEmpty(searchModelJson))
            {
                var dataSearch = JsonConvert.DeserializeObject<RoleSearchDto>(searchModelJson) ?? new RoleSearchDto();
                // tìm kiếm giữ lại pageSize và trả về trang 1
                if (!String.IsNullOrEmpty(search.Code))
                {
                    search.PageSize = dataSearch.PageSize;
                }
            }

            var data = _roleService.GetDataByPage(search);
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
                    if (_roleService.GetAll().Any(x => x.Code == model.Code))
                    {
                        result.ErrorMessage("Mã vai trò đã tồn tại!");
                        return result;
                    }

                    Role role = new Role()
                    {
                        Code = model.Code,
                        Name = model.Name
                    };
                    _roleService.Create(role);
                }
                else
                {
                    result.ErrorMessage("Thêm mới thất bại");
                    return result;
                }
            }
            catch (Exception)
            {
                result.ErrorMessage("Đã xảy ra lỗi");
                return result;
            }

            return result;
        }

        public IActionResult Edit(long id)
        {
            var editModal = new EditVM();
            var role = _roleService.GetById(id);
            editModal.Id = role.Id;
            editModal.Code = role.Code;
            editModal.Name = role.Name;
            return View(editModal);
        }

        [HttpPost]    
        public ResponseData Edit(EditVM model)
        {
            var result = new ResponseData() { status = true, message = "Cập nhật thành công" };
            try
            {
                if (ModelState.IsValid)
                {
                    if (_roleService.GetAll().Any(x => x.Code == model.Code && x.Id != model.Id))
                    {
                        result.ErrorMessage("Mã vai trò đã tồn tại!");
                        return result;
                    }

                    var role = _roleService.GetById(model.Id);

                    if (role == null)
                    {
                        result.ErrorMessage("Không tìm thấy bản ghi để sửa!");
                        return result;
                    }
                    role.Code = model.Code;
                    role.Name = model.Name;
                    _roleService.Update(role);
                }
                else
                {
                    result.ErrorMessage("Cập nhật thất bại");
                    return result;
                }
            }
            catch (Exception)
            {
                result.ErrorMessage("Đã xảy ra lỗi");
                return result;
            }

            return result;
        }
    
    
    }
}
