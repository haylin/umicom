using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umicom.Application.UserApp;
using WebApi.Models;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    public class LoginController : UmicomControllerBase
    {

        private IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">LoginModel</param>
        /// <returns>登录结果</returns>
        [HttpPost("UserLogin")]
        public async Task<JsonResult> UserLogin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result= await  _userAppService.CheckUser(model.UserName, model.Password);
                return result != null
                    ? new JsonResult(new ReturnValue()
                    {
                        ReturnCode = 1000,
                        ReturnMsg = "登陆成功",
                        IsSuccess = true
                    })
                    : new JsonResult(new ReturnValue()
                    {
                        ReturnCode = 11,
                        ReturnMsg = "用户名或者密码出错",
                        IsSuccess = false
                    });
            }
            else
            {
                return new JsonResult(new ReturnValue()
                {
                    ReturnCode = -1000,
                    ReturnMsg = "未知错误",
                    IsSuccess = false
                });
            }
        }



        // GET: api/Login/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Login
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Login/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
