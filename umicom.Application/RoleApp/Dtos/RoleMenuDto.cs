using System;
using System.Collections.Generic;
using System.Text;
using Umicom.Application.MenuApp.Dtos;

namespace Umicom.Application.RoleApp.Dtos
{
  public  class RoleMenuDto
    {
        public Guid RoleId { get; set; }
        public RoleDto Role { get; set; }

        public Guid MenuId { get; set; }
        public MenuDto Menu { get; set; }
    }
}
