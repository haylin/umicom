﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Umicom.Application.RoleApp.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "角色名称不能为空。")]
        public string Name { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }
    }
}