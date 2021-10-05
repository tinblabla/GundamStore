using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GundamShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }
        [Display(Name="Tên Đăng Nhập")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Tên Đăng Nhập")]
        public string UserName { set; get; }
        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Mật Khẩu")]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự")]
        public string Password { set; get; }
        [Display(Name = "Xác Nhận Mật Khẩu")]
        [Required(ErrorMessage = "Yêu Cầu Xác Nhận Lại Mật Khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Yêu Cầu Tên")]
        public string Name { set; get; }
        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Địa Chỉ")]
        public string Address { set; get; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Email")]
        public string Email { set; get; }
        [Display(Name = "Số Điện Thoại")]
        [Required(ErrorMessage = "Yêu Cầu Nhập Số Điện Thoại")]
        public string Phone { set; get; }
    }
}