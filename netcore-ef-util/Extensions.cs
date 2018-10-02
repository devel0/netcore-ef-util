using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SearchAThing.EFUtil
{

    public static partial class Util
    {

        /// <summary>
        /// execute Raw SQL queries: Non-model types
        /// https://github.com/aspnet/EntityFrameworkCore/issues/1862
        /// </summary>        
        public static List<T> ExecSQL<T>(this DbContext context, string query, ILogger logger = null)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var list = new List<T>();
                    var obj = default(T);

                    while (result.Read())
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            if (!Equals(result[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, result[prop.Name], null);
                            }
                        }
                        foreach (FieldInfo field in obj.GetType().GetFields())
                        {
                            if (!Equals(result[field.Name], DBNull.Value))
                            {
                                field.SetValue(obj, result[field.Name]);
                            }
                        }
                        list.Add(obj);
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
