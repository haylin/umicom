// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Umicom.Passport.Quickstart.UI
{
    public class LoginInputModel
    {
        [Required(ErrorMessage ="��������Ч���û�����")]
        [StringLength(160,MinimumLength =6,ErrorMessage ="�û����Ʋ�ӦС��6���ַ�")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{6,16}$", ErrorMessage = "�û�����4��16λ����ĸ�����֣��»��ߣ����ţ�")]
        [Display(Name ="�û���",Prompt ="�������û�����")]
        public string Username { get; set; }
        [Required(ErrorMessage = "��������Ч���û�����")]
        [StringLength(160, MinimumLength = 6, ErrorMessage = "��¼���벻ӦС��6���ַ�")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "��¼������ĸ��ͷ��������6~18֮�䣬ֻ�ܰ�����ĸ�����ֺ��»���")]
        [Display(Name ="��¼����",Prompt ="�������¼����")]        
        public string Password { get; set; }
        [Display(Name ="��ס��¼״̬")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}