using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SearchAThing.NETCoreEFUtil
{

    public static partial class Util
    {

        /// <summary>
        /// retrieve psql representation of boolean
        /// "TRUE" or "FALSE" string
        /// </summary>        
        public static string ToPsql(this bool value)
        {

            if (value)
                return "TRUE";
            else
                return "FALSE";
        }

        /// <summary>
        /// retrieve psql representation of boolean
        /// "TRUE" or "FALSE" string
        /// or "null" string if given argument is null
        /// </summary>        
        public static string ToPsql(this bool? value)
        {
            if (value.HasValue)
                return value.Value.ToPsql();
            else
                return "null";
        }

        /// <summary>
        /// retrieve psql representation of integer
        /// (number without quotes)        
        /// </summary>        
        public static string ToPsql(this int value)
        {
            return value.ToString();
        }

        /// <summary>
        /// retrieve psql representation of integer
        /// (number without quotes)
        /// of "null" string if given argument is null
        /// </summary>        
        public static string ToPsql(this int? value)
        {
            if (value.HasValue)
                return value.ToString();
            else
                return "null";
        }

        /// <summary>
        /// retrieve psql repsentation of string
        /// 'string' enquoted and escaped
        /// or "null" stringif given argument is null
        /// </summary>        
        public static string ToPsql(this string str)
        {
            if (str == null)
                return "null";
            else
                return $"'{str.Replace("'", "''")}'";
        }

        /// <summary>
        /// retrieve psql representation of datetime
        /// 'YYYY-MM-DD hh:mm:ss.millis'
        /// </summary>        
        public static string ToPsql(this DateTime dt)
        {
            return string.Format("'{0:0000}-{1:00}-{2:00} {3}'",
                dt.Year, dt.Month, dt.Day,
                string.Format(CultureInfo.InvariantCulture, "{0}", dt.TimeOfDay));
        }

        /// <summary>
        /// retrieve psql representation of datetime
        /// 'YYYY-MM-DD hh:mm:ss.millis'
        /// or "null" string if given argument is null        
        /// </summary>                
        public static string ToPsql(this DateTime? dt)
        {
            if (dt.HasValue)
                return dt.Value.ToPsql();
            else
                return "null";
        }

        /// <summary>
        /// retrieve psql representation of double
        /// (number without quotes)        
        /// </summary>        
        public static string ToPsql(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// retrieve psql representation of double
        /// (number without quotes)
        /// of "null" string if given argument is null
        /// </summary>        
        public static string ToPsql(this double? value)
        {
            if (value.HasValue)
                return value.Value.ToString(CultureInfo.InvariantCulture);
            else
                return "null";
        }

        /// <summary>
        /// retrieve psql representation of double array
        /// '{val1,val2,...,valn}'
        /// list enquoted, using invariant culture
        /// return "null" string if entire array is a null
        /// </summary>        
        public static string ToPsql(this IEnumerable<double> ary)
        {
            if (ary == null)
                return "null";
            else
            {
                var sb = new StringBuilder();

                sb.Append("'{");

                var en = ary.GetEnumerator();
                var first = true;

                while (en.MoveNext())
                {
                    if (!first)
                        sb.Append(',');
                    else
                        first = false;

                    sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}", en.Current));
                }

                sb.Append("}'");

                return sb.ToString();
            }
        }

        /// <summary>
        /// retrieve psql representation of nullable double array
        /// '{val1,val2,null,...,valn}'
        /// list enquoted, using invariant culture and evaluating null to "null" strings
        /// </summary>        
        public static string ToPsql(this IEnumerable<double?> ary)
        {
            if (ary == null)
                return "null";
            else
            {
                var sb = new StringBuilder();

                sb.Append("'{");

                var en = ary.GetEnumerator();
                var first = true;

                while (en.MoveNext())
                {
                    if (!first)
                        sb.Append(',');
                    else
                        first = false;

                    var cur = en.Current;

                    if (cur == null)
                        sb.Append("null");
                    else
                        sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}", cur.Value));
                }

                sb.Append("}'");

                return sb.ToString();
            }

        }

    }

}
