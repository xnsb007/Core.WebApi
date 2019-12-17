using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YunXun.Dapper.IRepository;
using YunXun.Entity.Models;
using YunXun.Entity.ViewModels;

namespace YunXun.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseResult<UserEntity>> Add(UserEntity entity)
        {
            return await userRepository.Add(entity);
        }

        /// <summary>
        /// 详细
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public async Task<BaseResult<UserEntity>> GetDetail(int id)
        {
            return await userRepository.Detail(id);
        }
    }
}