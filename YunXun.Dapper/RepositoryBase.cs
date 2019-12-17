using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using YunXun.Dapper.DataFactory;
using YunXun.Entity.ViewModels;

namespace YunXun.Dapper
{
    /// <summary>
    /// 仓储接口实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T, TKey> : IRepositoryBase<T,TKey> where T : class
    {
        protected DbOption dbOption;
        protected IDbConnection dbConnection;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="addStr">新增sql语句</param>
        /// <returns>返回主键值</returns>
        public async Task<int?> Add(T entity, string addStr)
        {
            return await dbConnection.ExecuteScalarAsync<int?>(addStr, entity);
        }

        /// <summary>
        /// 删除（批量删除）
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="deleteStr">删除sql语句</param>
        /// <returns>返回受影响行数</returns>
        public async Task<int?> Delete(object id, string deleteStr)
        {
            return await dbConnection.ExecuteScalarAsync<int?>(deleteStr, new { id = id });
        }

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <returns>受影响的行数</returns>
        public async Task<int?> DeleteBy(object conditionStr)
        {
            return await dbConnection.ExecuteScalarAsync<int?>(conditionStr.ToString());
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <param name="transaction">事物</param>
        /// <param name="commandTimeout">超时时间</param>
        /// <returns></returns>
        public async Task<int?> DeleteList(object conditionStr, IDbTransaction transaction = null)
        {
            return await dbConnection.ExecuteScalarAsync<int?>(conditionStr.ToString());
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns>对象实体</returns>
        public async Task<T> Detail(TKey id, string detailStr)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<T>(detailStr, new { id = id });
        }

        /// <summary>
        /// 按条件获取详情
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <returns>对象实体</returns>
        public async Task<T> DetailBy(object conditionStr)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<T>(conditionStr.ToString());
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="sqlStr">查询总数</param>
        /// <returns>返回总记录数</returns>
        public async Task<int?> GetCount(string sqlStr)
        {
            return await dbConnection.QuerySingleAsync<int?>(sqlStr);
        }

        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="sqlStr">分页查询语句</param>
        /// <returns>由于异步方法不能有out，ref  所以用了一个实体返回</returns>
        public async Task<PageData<T>> GetPageList(string sqlStr)
        {
            var reader = await dbConnection.QueryMultipleAsync(sqlStr.ToString());
            int retNum = await reader.ReadFirstAsync<int>();
            IEnumerable<T> tList = await reader.ReadAsync<T>();
            return new PageData<T>() { list = tList.ToList(), total = retNum };
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="selectStr">查询sql语句</param>
        /// <returns></returns>
        public async Task<IList<T>> Select(string selectStr)
        {
            IEnumerable<T> tList = await Task.Run(() => dbConnection.QueryAsync<T>(selectStr));
            return tList.ToList();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateStr">更新sql语句</param>
        /// <returns></returns>
        public async Task<int?> Update(T entity, string updateStr)
        {
            return await dbConnection.ExecuteScalarAsync<int?>(updateStr, entity);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~RepositoryBase()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
