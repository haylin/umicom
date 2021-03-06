﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;

namespace Umicom.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 用户管理仓储实现
    /// </summary>
    public class UserRepository : UmicomRepositoryBase<User>, IUserRepository
    {
        public UserRepository(UmicomContext dbcontext) : base(dbcontext)
        {
        }

        /// <summary>
        /// 检查用户是存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>存在返回用户实体，否则返回NULL</returns>
        public async Task<User> CheckUser(string userName, string password)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(it => it.UserName == userName && it.Password == password);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public async Task<User> GetWithRoles(Guid id)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(it => it.Id == id);
            if (user != null)
            {
                user.UserRoles = _dbContext.Set<UserRole>().Where(it => it.UserId == id).ToList();
            }
            return user;
        }
    }
}