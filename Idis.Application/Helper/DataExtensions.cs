using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Idis.Application
{
    public static class DataExtensions
    {
        public static string Dump(object anObject)
        {
            return JsonConvert.SerializeObject(anObject);
        }

        public static List<T> ConvertDataTable<T>(DataTable dTable)
        {
            List<T> t_list = new();
            foreach (DataRow row in dTable.Rows)
            {
                T item = GetItem<T>(row);
                t_list.Add(item);
            }
            return t_list;
        }

        public static T GetItem<T>(DataRow dRow)
        {
            T t_object = Activator.CreateInstance<T>();
            Type tType = typeof(T);

            foreach (DataColumn column in dRow.Table.Columns)
            {
                foreach (PropertyInfo pInfo in tType.GetProperties())
                {
                    if (pInfo.Name == column.ColumnName)
                        pInfo.SetValue(
                            t_object,
                            dRow[column.ColumnName] == DBNull.Value ?
                            "-" : dRow[column.ColumnName],
                            null);
                    else
                        continue;
                }
            }
            return t_object;
        }
    }
}