using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Umicom.Application.MenuApp;
using Umicom.Application.MenuApp.Dtos;
using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;

namespace Umicom.Application
{
    public class MenuAppService : IMenuAppService
    {
        //用户管理仓储接口
        private readonly IMenuRepository _menuRepository;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 构造函数 实现依赖注入
        /// </summary>
        /// <param name="userRepository">仓储对象</param>
        public MenuAppService(IMenuRepository menuRepository, IUserRepository userRepository,IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void Delete(Guid id)
        {
            _menuRepository.Delete(id);
        }

        public void DeleteBatch(List<Guid> ids)
        {
            _menuRepository.Delete(_ => ids.Contains(_.Id));
        }

        public MenuDto Get(Guid id)
        {
            return Mapper.Map<MenuDto>(_menuRepository.Get(id));
        }

        public MenuDto GetObject(string code)
        {
            return Mapper.Map<MenuDto>(_menuRepository.GetObject(code));
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            //使用AutoMapper进行实体转换
            return Mapper.Map<List<MenuDto>>(menus);
        }

        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<MenuDto> GetMenusByUser(Guid userId)
        {
            List<MenuDto> result = new List<MenuDto>();
            var allMenus = _menuRepository.GetAllList(it => it.Type == 0).OrderBy(it => it.SerialNumber);
            if (userId == Guid.Empty) //超级管理员
                return Mapper.Map<List<MenuDto>>(allMenus);
            var user = _userRepository.GetWithRoles(userId);
            if (user == null)
                return result;
            var userRoles = user.UserRoles;
            List<Guid> menuIds = new List<Guid>();
            foreach (var role in userRoles)
            {
                 menuIds = menuIds.Union(_roleRepository.GetAllMenuListByRole(role.RoleId)).ToList();
            }
            allMenus = allMenus.Where(it => menuIds.Contains(it.Id)).OrderBy(it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(allMenus);
        }

        public List<MenuDto> GetMneusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu == null ? false : true;
        }
    }
}