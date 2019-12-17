using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Common
{
    public class NlogHelper
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="throwMsg">错误信息</param>
        /// <param name="ex"></param>
        public static void ErrorLog(string throwMsg, Exception ex)
        {
            string errorMsg = string.Format("【异常信息】：{0} <br>【异常类型】：{1} <br>【堆栈调用】：{2}",
                new object[] { throwMsg, ex.GetType().Name, ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            logger.Error(errorMsg);
        }

        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="operateMsg">操作信息</param>
        public static void InfoLog(string operateMsg)
        {
            string errorMsg = string.Format("【操作信息】：{0} <br>",
                new object[] { operateMsg });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            logger.Info(errorMsg);
        }
    }
}
