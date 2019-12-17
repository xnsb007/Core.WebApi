using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YunXun.Jwt;

namespace YunXun.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IJwt _jwt;
        public AuthController(IJwt jwt)
        {
            this._jwt = jwt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetToken(UserInfo userInfo)
        {
            if (true)
            {
                Dictionary<string, string> clims = new Dictionary<string, string>();
                clims.Add("userName", userInfo.userName);
                return new JsonResult(this._jwt.GetToken(clims));
            }
        }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        public string userName { get; set; }
        public string passWord { get; set; }
    }
}