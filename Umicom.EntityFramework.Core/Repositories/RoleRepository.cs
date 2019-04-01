﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;

namespace Umicom.EntityFrameworkCore.Repositories
{
   public class RoleRepository : UmicomRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(UmicomContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId">todo: describe roleId parameter on GetAllMenuListByRole</param>
        /// <returns></returns>
        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            var roleMenus = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId);
            var menuIds = from t in roleMenus select t.MenuId;
            return menuIds.ToList();
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus)
        {
            var oldDatas = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId).ToList();
            oldDatas.ForEach(it => _dbContext.Set<RoleMenu>().Remove(it));
            _dbContext.SaveChanges();
            _dbContext.Set<RoleMenu>().AddRange(roleMenus);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
