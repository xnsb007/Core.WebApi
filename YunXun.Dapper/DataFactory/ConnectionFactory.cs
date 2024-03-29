﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace YunXun.Dapper.DataFactory
{
    public class ConnectionFactory
    {
     /// <summary>
     /// 获取数据库连接
     /// </summary>
     /// <param name="dbtype">数据库类型</param>
     /// <param name="conStr">数据库连接字符串</param>
     /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(string dbtype, string strConn)
        {
            if (string.IsNullOrEmpty(dbtype))
            {
                throw new ArgumentNullException("请传数据库类型");
            }
            if (string.IsNullOrEmpty(strConn))
            {
                throw new ArgumentNullException("请传数据库连接字符串");
            }
            var dbType = GetDataBaseType(dbtype);
            return CreateConnection(dbType, strConn);
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="conStr">数据库连接字符串</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection CreateConnection(DatabaseType dbType, string strConn)
        {
            IDbConnection connection = null;
            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(strConn);
                    break;
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(strConn);
                    break;
                default:
                    throw new ArgumentNullException("暂时不支持其他数据类型！");
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype">数据库类型字符串</param>
        /// <returns>数据库类型</returns>
        public static DatabaseType GetDataBaseType(string dbtype)
        {
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                if (dbType.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }
            return returnValue;
        }
    }
}