using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Jwt
{
    public class JwtEntity
    {
        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 使用者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 加密key
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 生命周期
        /// </summary>
        public int Lifetime { get; set; }
        /// <summary>
        /// 是否验证生命周期
        /// </summary>
        public bool ValidateLifetime { get; set; }
        /// <summary>
        /// 验证头字段
        /// </summary>
        public string HeadField { get; set; }
        /// <summary>
        /// jwt验证前缀
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// 忽略验证的url
        /// </summary>
        public List<string> IgnoreUrls { get; set; }

    }
}
