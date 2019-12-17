using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YunXun.Common;
using YunXun.Dapper.DataFactory;
using YunXun.Dapper.IRepository;
using YunXun.Entity.Models;
using YunXun.Entity.ViewModels;

namespace YunXun.Dapper.Repository
{
    public class UserRepository : RepositoryBase<UserEntity,int>, IUserRepository
    {
        public UserRepository(IOptionsSnapshot<DbOption> options)
        {
            dbOption = options.Get("DBConnect");
            if (dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            dbConnection = ConnectionFactory.CreateConnection(dbOption.DbType, dbOption.ConnectionString);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>返回主键值</returns>
        public async Task<BaseResult<UserEntity>> Add(UserEntity entity)
        {
            string insertSql = @"INSERT INTO [dbo].[tbUser](userName, passWord,age) VALUES(@userName, @passWord,@age) select @@identity";
            var result = new BaseResult<UserEntity>();
            try
            {
                var i = await Add(entity, insertSql);
                if (null != i && i > 0)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception ex)
            {
                NlogHelper.InfoLog(ex.Message);
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">标识</param>
        /// <returns>影响的行数</returns>
        public async Task<BaseResult<UserEntity>> Delete(int Id)
        {
            string deleteSql = "update [dbo].[tbUser] SET isDelete=1  WHERE userId=@userId";
            var result = new BaseResult<UserEntity>();
            try
            {
                var i = await Delete(Id, deleteSql);
                if (null != i && i > 0)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Id">标识</param>
        /// <returns>影响的行数</returns>
        public async Task<BaseResult<UserEntity>> DeleteList(int[] Ids)
        {
            string deleteSql = "update [dbo].[tbUser] SET isDelete=1  WHERE userId in @userIds";
            var result = new BaseResult<UserEntity>();
            try
            {
                var i = await Delete(Ids, deleteSql);
                if (null != i && i > 0)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="Id">主键标识</param>
        /// <returns>受影响都行数</returns>
        public async Task<BaseResult<UserEntity>> DeleteBy(string conditionStr)
        {
            string deleteSql = "UPDATE [dbo].[tbUser] SET IsDelete=1 WHERE 1=1 and  " + conditionStr;
            var result = new BaseResult<UserEntity>();
            try
            {
                var i = await DeleteBy(deleteSql);
                if (null != i)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public async Task<BaseResult<UserEntity>> Update(UserEntity entity)
        {
            string updateSql = @"UPDATE [dbo].[tbUser] SET userName=@userName,passWord=@passWord,@age=@age where userId=@userId";
            var result = new BaseResult<UserEntity>();
            try
            {
                var i = await Update(entity, updateSql);
                if (null != i)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public async Task<BaseResult<UserEntity>> GetList()
        {
            string selectSql = @"SELECT userId, userName, passWord, age FROM [dbo].[tbUser] where IsDelete=0";
            var result = new BaseResult<UserEntity>();
            try
            {
                var qList = await Select(selectSql);
                if (null != qList)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.data = new PageData<UserEntity> { list = qList, total = 1 };
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
            return result;
        }

        /// <summary>
        /// 获取详细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<BaseResult<UserEntity>> Detail(int Id)
        {
            string detailSql = @"SELECT userId, userName, passWord, age FROM [dbo].[tbUser] WHERE userId=@userId";
            var result = new BaseResult<UserEntity>();
            try
            {
                var entity = await Detail(Id, detailSql);
                if (null != entity)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.data = new PageData<UserEntity> { list = new List<UserEntity>() { entity }, total = 1 };
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (System.Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }
           
            return result;
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <returns>返回记录总数</returns>
        public async Task<BaseResult<UserEntity>> GetCount()
        {
            string countSql = @"SELECT count(userId) FROM [dbo].[tbUser] where IsDelete=0";
            var result = new BaseResult<UserEntity>();
            try
            {
                var entity = await GetCount(countSql);
                if (null != entity)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.data = new PageData<UserEntity> { list = { }, total = 1 };
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (System.Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }

            return result;
        }

        /// <summary>
        ///   获取分页数据
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns></returns>
        public async Task<BaseResult<UserEntity>> GetListByPage(int pageIndex, int pageSize, int SortRule = 1)
        {
            string tableName = "tbUser";
            string where = " IsDelete=0";
            string orderby = "userId ";
            if (SortRule == 1)
            {
                orderby += " desc";
            }
            else
            {
                orderby += " asc";
            }
            pageSize = pageSize < 1 ? 10 : pageSize;
            int skip = 1;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize + 1;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0} where {1};", tableName, where);
            sb.AppendFormat(@"SELECT  *
                                FROM(SELECT ROW_NUMBER() OVER(ORDER BY {2}) AS RowNum,*
                                          FROM  {0}
                                          WHERE {1}
                                        ) AS result
                                WHERE  RowNum >= {3}   AND RowNum <= {4}
                                ORDER BY {2}", tableName, where, orderby, skip, pageIndex * pageSize);
            var result = new BaseResult<UserEntity>();
            try
            {
                var pageData = await GetPageList(sb.ToString());
                if (null != pageData)
                {
                    result.code = ResultKey.RETURN_SUCCESS_CODE;
                    result.data = pageData;
                    result.desc = ResultKey.RETURN_SUCCESS_DESC;
                }
                else
                {
                    result.code = ResultKey.RETURN_FAIL_CODE;
                    result.desc = ResultKey.RETURN_FAIL_DESC;
                }
            }
            catch (System.Exception)
            {
                result.code = ResultKey.RETURN_EXCEPTION_CODE;
                result.desc = ResultKey.RETURN_EXCEPTION_DESC;
            }

            return result;
        }
    }
}
