using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Umicom.Application.RoleApp.Dtos;
using Umicom.Domain.Entities;
using Umicom.Domain.IRepositories;

namespace Umicom.Application.RoleApp
{
    public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepository _repository;

        public RoleAppService(IRoleRepository roleRepository)
        {
            _repository = roleRepository;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">Id</param>
        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// 根据Id集合批量删除
        /// </summary>
        /// <param name="ids">Id集合</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteBatch(List<Guid> ids)
        {
            _repository.Delete(x => ids.Contains(x.Id));
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public RoleDto Get(Guid id)
        {
            return Mapper.Map<RoleDto>(_repository.Get(id));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<RoleDto> GetAllList()
        {
            return Mapper.Map<List<RoleDto>>(_repository.GetAllList().OrderBy(x => x.Code));
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            return _repository.GetAllMenuListByRole(roleId);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<RoleDto> GetAllPageList(int startPage, int pageSize, out int rowCount)
        {
            return Mapper.Map<List<RoleDto>>(_repository.LoadPageList(startPage, pageSize, out rowCount, null, it => it.Code));
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool InsertOrUpdate(RoleDto dto)
        {
            var menu = _repository.InsertOrUpdate(Mapper.Map<Role>(dto));
            return menu == null ? false : true;
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateRoleMenu(Guid roleId, List<RoleMenuDto> roleMenus)
        {
            return _repository.UpdateRoleMenu(roleId, Mapper.Map<List<RoleMenu>>(roleMenus));
        }
    }
}