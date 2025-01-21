using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Newtonsoft.Json;
using Service.UserService;
using Service.UserService.Dto;
using Web.Models.Common;
using Web.Models.User;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
        }
        public IActionResult Index()
        {
            var searchModel = new UserSearchDto();
            var data = _UserService.GetDataByPage(searchModel);
            return View(data);
        }

        // Dùng để cập nhật lại table data sau khi CRUD: giữ lại searchmodel
        public IActionResult Refresh()
        {
            var searchModel = new UserSearchDto();
            // lấy lại search để giữ nguyên màn hình trang
            var searchModelJson = HttpContext.Session.GetString("SearchModel");
            if (!String.IsNullOrEmpty(searchModelJson))
            {
                searchModel = JsonConvert.DeserializeObject<UserSearchDto>(searchModelJson) ?? new UserSearchDto();
            }

            var data = _UserService.GetDataByPage(searchModel);

            return PartialView("_ViewData", data);
        }

        // Được gọi khi thao tác với pagination
        public IActionResult GetData(int PageIndex, int PageSize)
        {
            var searchModelJson = HttpContext.Session.GetString("SearchModel");
            var searchModel = new UserSearchDto();
            // chuyển trang vẫn giữ lại phần tìm kiếm 
            if (!String.IsNullOrEmpty(searchModelJson))
            {
                searchModel = JsonConvert.DeserializeObject<UserSearchDto>(searchModelJson) ?? new UserSearchDto();
            }
            // chỉ thay đổi trang và pageSize
            searchModel.PageIndex = PageIndex;
            searchModel.PageSize = PageSize;

            HttpContext.Session.SetString("SearchModel", JsonConvert.SerializeObject(searchModel));

            var data = _UserService.GetDataByPage(searchModel);
            return PartialView("_ViewData", data);
        }

        // Được gọi khi tìm kiếm
        public IActionResult SearchData(UserSearchDto searchModel)
        {
            var search = new UserSearchDto();
            if (searchModel != null)
            {
                search = searchModel;
            }
            var searchModelJson = HttpContext.Session.GetString("SearchModel");
            if (!String.IsNullOrEmpty(searchModelJson))
            {
                var dataSearch = JsonConvert.DeserializeObject<UserSearchDto>(searchModelJson) ?? new UserSearchDto();
                // tìm kiếm giữ lại pageSize và trả về trang 1
                search.PageSize = dataSearch.PageSize;
            }
            HttpContext.Session.SetString("SearchModel", JsonConvert.SerializeObject(search));

            var data = _UserService.GetDataByPage(search);
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
                    if (_UserService.GetAll().Any(x => x.UserName == model.UserName))
                    {
                        result.ErrorMessage("Tên người dùng đã tồn tại!");
                        return result;
                    }

                    User User = new User()
                    {
                        Email = model.Email,
                        UserName = model.UserName,
                        Fullname = model.Fullname,
                        Phone = model.Phone
                    };
                    _UserService.Create(User);
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
            var User = _UserService.GetById(id);
            editModal.Id = User.Id;
            editModal.Phone = User.Phone;
            editModal.Fullname = User.Fullname;
            editModal.UserName = User.UserName;
            editModal.Email = User.Email;
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
                    if (_UserService.GetAll().Any(x => x.UserName == model.UserName && x.Id != model.Id))
                    {
                        result.ErrorMessage("Tên người dùng đã tồn tại!");
                        return result;
                    }

                    var User = _UserService.GetById(model.Id);

                    if (User == null)
                    {
                        result.ErrorMessage("Không tìm thấy bản ghi để sửa!");
                        return result;
                    }

                    User.Email = model.Email;
                    User.Fullname  = model.Fullname;
                    User.Phone = model.Phone;
                    User.UserName = model.UserName;

                    _UserService.Update(User);
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

        [HttpGet]
        public ResponseData Delete(long id)
        {
            var result = new ResponseData() { status = true, message = "Xóa thành công" };
            try
            {
                var item = _UserService.GetById(id);
                if (item == null)
                {
                    result.ErrorMessage("Không tìm thấy thông tin cần xóa!");
                    return result;
                }

                _UserService.Delete(item.Id);
            }
            catch (Exception)
            {
                result.ErrorMessage("Đã xảy ra lỗi khi xóa!");
            }
            return result;
        }
    }
}
