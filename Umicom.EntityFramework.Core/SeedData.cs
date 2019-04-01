using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Umicom.Domain.Entities;

namespace Umicom.EntityFrameworkCore
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UmicomContext(serviceProvider.GetRequiredService<DbContextOptions<UmicomContext>>()))
            {
                string baseId = "00000000-0000-0000-0000-000000000000";
                //判断是否有待迁移
                if (context.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("Migrating...");
                    //执行迁移
                    context.Database.Migrate();
                    Console.WriteLine("Migrated");
                }
                if (context.Users.Any())
                {
                    return;   // 已经初始化过数据，直接返回
                }

                Guid departmentId = Guid.NewGuid();

                var department = new Department
                {
                    Id = departmentId,
                    Name = "默认用户",
                    ParentId = Guid.Empty
                };

                var users = new User
                {
                    Id = Guid.Parse(baseId),
                    UserName = "admin",
                    Password = "123456", //暂不进行加密
                    Name = "超级管理员",
                    DepartmentId = departmentId
                };
                var roles = new Role()
                {
                    Id = Guid.Parse(baseId),
                    Name = "默认",
                    Code = "1000",
                    CreateTime = DateTime.Now,
                    CreateUserId = Guid.Empty
                };
                var newBaseId = baseId.Remove(baseId.Length - 1);
                var menus = new List<Menu>()
                {
                    new Menu
                    {
                        Id =Guid.Parse(newBaseId+"1"),
                        Name = "组织机构管理",
                        Code = "Department",
                        SerialNumber = 0,
                        ParentId = Guid.Empty,
                        Url = "/Department",
                        Icon = "fa fa-link"
                    },
                    new Menu
                    {
                        Id =Guid.Parse(newBaseId+"2"),
                        Name = "角色管理",
                        Code = "Role",
                        SerialNumber = 1,
                        ParentId = Guid.Empty,
                        Url = "/Role",
                        Icon = "fa fa-link"
                    },
                    new Menu
                    {
                        Id =Guid.Parse(newBaseId+"3"),
                        Name = "用户管理",
                        Code = "User",
                        SerialNumber = 2,
                        ParentId = Guid.Empty,
                        Url = "/User",
                        Icon = "fa fa-link"
                    },
                    new Menu
                    {
                        Id =Guid.Parse(newBaseId+"4"),
                        Name = "功能管理",
                        Code = "Menu",
                        SerialNumber = 3,
                        ParentId = Guid.Empty,
                        Url = "/Menu",
                        Icon = "fa fa-link"
                    }
                };
                // var roleMenu=new RoleMenu();

                //增加一个部门
                context.Departments.Add(department);

                //增加四个基本功能菜单
                context.Menus.AddRange(menus);

                //增加一个角色
                context.Roles.Add(roles);

                //设置角色所在的菜单
                context.RoleMenus.AddRange(new List<RoleMenu>()
                {
                    new RoleMenu()
                    {
                        RoleId =roles.Id,
                        MenuId =Guid.Parse(newBaseId+"1")
                    },
                    new RoleMenu()
                    {
                        RoleId =roles.Id,
                        MenuId =Guid.Parse(newBaseId+"2")},
                    new RoleMenu()
                    {
                        RoleId =roles.Id,
                        MenuId =Guid.Parse(newBaseId+"3")
                    },
                    new RoleMenu()
                    {
                        RoleId =roles.Id,
                        MenuId =Guid.Parse(newBaseId + "4")
                    }
                });

                //增加一个超级管理员用户
                context.Users.Add(users);

                //设置当前用户所属角色
                context.UserRoles.Add(new UserRole()
                {
                    UserId = users.Id,
                    RoleId = roles.Id
                });

                context.SaveChanges();
            }
        }
    }
}