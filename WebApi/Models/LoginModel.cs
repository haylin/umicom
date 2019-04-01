using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空。")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空。")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住当前用户
        /// </summary>
        public bool RememberMe { get; set; }
    }
}