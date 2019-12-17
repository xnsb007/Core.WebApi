using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Entity.ViewModels
{
    public class ResultKey
    {
        /// <summary>
        ///  操作成功返回码
        /// </summary>
        public const string RETURN_SUCCESS_CODE = "0";
        /// <summary>
        /// 操作成功描述
        /// </summary>
        public const string RETURN_SUCCESS_DESC = "操作成功";
        /// <summary>
        /// 操作失败返回码
        /// </summary>
        public const string RETURN_FAIL_CODE = "-1";
        /// <summary>
        /// 操作失败描述
        /// </summary>
        public const string RETURN_FAIL_DESC = "操作失败";
        /// <summary>
        /// 系统异常返回码
        /// </summary>
        public const string RETURN_EXCEPTION_CODE = "-2";
        /// <summary>
        /// 操作失败描述
        /// </summary>
        public const string RETURN_EXCEPTION_DESC = "操作异常";
    }
}
