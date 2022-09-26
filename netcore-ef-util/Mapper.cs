using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SearchAThing.Helpers;

namespace SearchAThing.Mapper
{
    // danke schoen https://codereview.stackexchange.com/a/98736
    public class DataReaderMapper<T>
    {
        Dictionary<int, Either<FieldInfo, PropertyInfo>> mappings;
        bool IsPrimitiveish;
        public DataReaderMapper(IDataReader reader)
        {
            Type U = Nullable.GetUnderlyingType(typeof(T));
            this.IsPrimitiveish = (
                typeof(T).IsPrimitive ||
                (U != null && U.IsPrimitive)
            );
            if (!this.IsPrimitiveish)
            {
                this.mappings = Mappings(reader);
            }
        }

        public class MapMismatchException : Exception
        {
            public MapMismatchException(string arg) : base(arg) { }
        }

        private class JoinInfo
        {
            public Either<FieldInfo, PropertyInfo> info;
            public String name;
        }
        // int keys are column indices (ordinals)
        static Dictionary<int, Either<FieldInfo, PropertyInfo>> Mappings(IDataReader reader)
        {
            var columns = Enumerable.Range(0, reader.FieldCount);
            var fieldsAndProps = typeof(T).FieldsAndProps()
                .Select(fp => fp.Match(
                    f => new JoinInfo { info = f, name = f.Name },
                    p => new JoinInfo { info = p, name = p.Name }
                ));
            var joined = columns
                  .Join(fieldsAndProps, reader.GetName, x => x.name, (index, x) => new
                  {
                      index,
                      x.info
                  }, StringComparer.InvariantCultureIgnoreCase).ToList();
            if (columns.Count() != joined.Count() || fieldsAndProps.Count() != joined.Count())
            {
                throw new MapMismatchException($"Expected to map every column in the result. " +
                    $"Instead, {columns.Count()} columns and {fieldsAndProps.Count()} fields produced only {joined.Count()} matches. " +
                    $"Hint: be sure all your columns have _names_, and the names match up.\r\n\r\n" +
                    $"columns: {string.Join(", ", columns.Select(cidx => $"{reader.GetName(cidx)} [{reader.GetFieldType(cidx)}]"))}\r\n\r\n" +
                    $"jioned : {string.Join(", ", typeof(T).FieldsAndPropNameTypes())}");
            }
            return joined
                 .ToDictionary(x => x.index, x => x.info);
        }

        public T MapFrom(IDataRecord record)
        {
            if (IsPrimitiveish)
            {
                // Primitive values will always have a single column, indexed by 0
                return (T)record.GetValue(0);
            }
            var element = Activator.CreateInstance<T>();
            foreach (var map in mappings)
                map.Value.Match(
                    f => f.SetValue(element, ChangeType(record[map.Key], f.FieldType)),
                    p => p.SetValue(element, ChangeType(record[map.Key], p.PropertyType))
                );

            return element;
        }

        static object ChangeType(object value, Type targetType)
        {
            if (value == null || value == DBNull.Value)
                return null;

            return Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType) ?? targetType);
        }
    }

    public static class FieldAndPropsExtension
    {
        public static IEnumerable<Either<FieldInfo, PropertyInfo>> FieldsAndProps(this Type T)
        {
            var lst = new List<Either<FieldInfo, PropertyInfo>>();
            lst.AddRange(
                T.GetFields()
                .Select(field => new Either<FieldInfo, PropertyInfo>.Left(field))
            );
            lst.AddRange(
                T.GetProperties()
                .Select(prop => new Either<FieldInfo, PropertyInfo>.Right(prop))
            );
            return lst;
        }

        public static IEnumerable<string> FieldsAndPropNames(this Type T)
        {
            foreach (var x in T.GetFields().Select(field => field.Name))
                yield return x;

            foreach (var x in T.GetProperties().Select(prop => prop.Name))
                yield return x;
        }
        public static IEnumerable<string> FieldsAndPropNameTypes(this Type T)
        {
            foreach (var x in T.GetFields())
                yield return $"{x.Name}[{x.FieldType}]";

            foreach (var x in T.GetProperties())
                yield return $"{x.Name}[{x.PropertyType}]";
        }


    }


}