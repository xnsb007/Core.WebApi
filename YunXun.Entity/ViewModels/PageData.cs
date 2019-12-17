using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Entity.ViewModels
{
    public partial class PageData<T>
    {
        /// <summary>
        /// 查询的分页数据
        /// </summary>
        public IList<T> list { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }
    }
}
