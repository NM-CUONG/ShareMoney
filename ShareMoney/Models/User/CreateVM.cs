using System.ComponentModel.DataAnnotations;

namespace Web.Models.User
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string Fullname { get; set; }
    }
}
