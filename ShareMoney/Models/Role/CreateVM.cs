using System.ComponentModel.DataAnnotations;

namespace Web.Models.Role
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này!")]
        public string? Name { get; set; }
    }
}
