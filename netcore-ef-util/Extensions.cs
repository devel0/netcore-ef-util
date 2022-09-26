using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SearchAThing.Mapper;

namespace SearchAThing.EFUtil
{

    public static partial class Util
    {

        public static List<T> ExecSQL<T>(this DbContext context, string query, ILogger logger = null)
        {
            return ExecSQL<T>(context, query, logger, null);
        }

        public static List<T> ExecSQL<T>(this DbContext context, string query, params SqlParameter[] sqlParams)
        {
            return ExecSQL<T>(context, query, null, sqlParams);
        }

        /// <summary>
        /// execute Raw SQL queries: Non-model types
        /// https://github.com/aspnet/EntityFrameworkCore/issues/1862
        /// </summary>
        public static List<T> ExecSQL<T>(this DbContext context, string query, ILogger logger, params SqlParameter[] sqlParams)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                if (sqlParams != null)
                {
                    foreach (var sqlParam in sqlParams)
                    {
                        var param = command.CreateParameter();
                        param.ParameterName = sqlParam.ParameterName;
                        param.DbType = sqlParam.DbType;
                        param.Value = sqlParam.Value;
                        command.Parameters.Add(param);
                    }
                }
                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var list = new List<T>();
                    var mapper = new DataReaderMapper<T>(result);

                    while (result.Read())
                    {
                        list.Add(mapper.MapFrom(result));
                    }

                    sw.Stop();
                    logger?.LogInformation($"Executed ({sw.ElapsedMilliseconds}ms)");
                    logger?.LogInformation($"{query}");

                    return list;
                }
            }
        }

    }

}
