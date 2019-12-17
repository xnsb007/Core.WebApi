using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YunXun.Entity.ViewModels;

namespace YunXun.Dapper
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T, TKey>: IDisposable where T:class
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="addStr">sql语句</param>
        /// <returns>返回主键值(自增类型返回主键值，否则返回null)</returns>
        Task<int?> Add(T entity, string addStr);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <param name="deleteStr">sql语句（软删除，硬删除）</param>
        /// <returns>影响的行数</returns>
        Task<int?> Delete(object id, string deleteStr);

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <returns></returns>
        Task<int?> DeleteBy(object conditionStr);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<int?> DeleteList(object conditionStr, IDbTransaction transaction = null);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateStr">sql语句</param>
        /// <returns>返回影响的行数</returns>
        Task<int?> Update(T entity, string updateStr);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="selectStr">sql语句</param>
        /// <returns>实体列表<T></returns>
        Task<IList<T>> Select(string selectStr);

        /// <summary>
        /// 实体详情
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <param name="detailStr"></param>
        /// <returns>返回实体T</returns>
        Task<T> Detail(TKey id, string detailStr);

        /// <summary>
        /// 按条件查询实体详情
        /// </summary>
        /// <param name="conditionStr">条件</param>
        /// <returns>返回数据实体</returns>
        Task<T> DetailBy(object conditionStr);

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <returns>返回记录总数</returns>
        Task<int?> GetCount(string sqlStr);


        /// <summary>
        ///  分页获取数据列表
        /// </summary>
        /// <param name="tableName">表名</param
        /// <returns>返回封装的分页实体</returns>
        Task<PageData<T>> GetPageList(string sqlStr);
    }
}
