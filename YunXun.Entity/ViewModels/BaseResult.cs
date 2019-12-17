using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Entity.ViewModels
{
    public class BaseResult<T>
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回的主要数据实体
        /// </summary>
        public PageData<T> data { get; set; }
        /// <summary>
        /// 返回值描述
        /// </summary>

        public string desc { get; set; }
    }
}
