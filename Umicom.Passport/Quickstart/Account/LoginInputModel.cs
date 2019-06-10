// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Umicom.Passport.Quickstart.UI
{
    public class LoginInputModel
    {
        [Required(ErrorMessage ="请输入有效的用户名称")]
        [StringLength(160,MinimumLength =6,ErrorMessage ="用户名称不应小于6个字符")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{6,16}$", ErrorMessage = "用户名称4到16位（字母，数字，下划线，减号）")]
        [Display(Name ="用户名",Prompt ="请输入用户名称")]
        public string Username { get; set; }
        [Required(ErrorMessage = "请输入有效的用户密码")]
        [StringLength(160, MinimumLength = 6, ErrorMessage = "登录密码不应小于6个字符")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "登录密码字母开头，长度在6~18之间，只能包含字母、数字和下划线")]
        [Display(Name ="登录密码",Prompt ="请输入登录密码")]        
        public string Password { get; set; }
        [Display(Name ="记住登录状态")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}