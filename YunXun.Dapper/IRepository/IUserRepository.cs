using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunXun.Entity.Models;
using YunXun.Entity.ViewModels;

namespace YunXun.Dapper.IRepository
{
    public interface IUserRepository : IRepositoryBase<UserEntity,int>
    {
        Task<BaseResult<UserEntity>> Add(UserEntity entity);

        Task<BaseResult<UserEntity>> Delete(int Id);

        Task<BaseResult<UserEntity>> Update(UserEntity entity);

        Task<BaseResult<UserEntity>> Detail(int Id);

        Task<BaseResult<UserEntity>> GetList();

        Task<BaseResult<UserEntity>> GetListByPage(int pageIndex, int pageSize, int SortRule);
    }
}
