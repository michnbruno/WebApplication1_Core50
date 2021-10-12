//using EFCore.CodeFirst.WebApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1_Core50.Contexts
{
    public static class DbContextExtensions
    {

        public static Type GetPrimaryKeyType(DbContext ctx, string table)
        {
            Type TableType = ctx.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);
            var props = ctx.Model.FindEntityType(TableType).FindPrimaryKey().Properties;
            var KeyType = props[0].ClrType;
            return KeyType;
        }
        public static string GetPrimaryKeyName(DbContext ctx, string table)
        {
            Type TableType = ctx.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);
            var props = ctx.Model.FindEntityType(TableType).FindPrimaryKey().Properties;
            var KeyName = props[0].Name;
            return KeyName;
        }

        public static dynamic GetPrimaryKeyRecordValue(dynamic typedEntityRecord, string primaryKeyName)
        {
            dynamic primaryKeyRecordValue;
            foreach (PropertyInfo prop in typedEntityRecord.GetType().GetProperties())
            {
                if (prop.Name == primaryKeyName)
                {
                    primaryKeyRecordValue = prop.GetValue(typedEntityRecord, null);
                    return primaryKeyRecordValue;
                }

            }
            primaryKeyRecordValue = null;
            return primaryKeyRecordValue;

            //Type TableType = ctx.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);
            //var props = ctx.Model.FindEntityType(TableType).FindPrimaryKey().Properties;
            //var KeyName = props[0].Name;
            //return KeyName;
        }

        public static dynamic ConvertKeyType(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }

        public static dynamic GetUpdateAddObject(dynamic updateRecord, dynamic entityStub)
        {
            Newtonsoft.Json.Linq.JObject jobj3 = Newtonsoft.Json.Linq.JObject.FromObject(updateRecord, new Newtonsoft.Json.JsonSerializer { NullValueHandling = NullValueHandling.Ignore });
            System.Type returnType = entityStub.GetType();
            dynamic dd3 = jobj3.ToObject(returnType, new Newtonsoft.Json.JsonSerializer { NullValueHandling = NullValueHandling.Ignore });

            return dd3;
        }
        public static Type GetEntityType(Assembly assembly, string entityNameSingular)
        {//

            


            dynamic a2 = null;

            foreach (Type t in assembly.GetExportedTypes())
            {
                var test = t;

                if (t.Name == entityNameSingular)
                {
                    a2 = t;
                    return a2;
                }
                else
                {
                    a2 = null;
                }

                Console.WriteLine(t);
            }
            // Type newt = a2;
            return a2;

        }


        //public List<TEntity> SqlQueryList(DbContext ctx, string sql, params object[] parameters)
        //{

        //    var ret = ctx.SqlQuery(sql, parameters);
        //    var test = ret.ToList();
        //    return test;
        //    //  return ret;
        //}

        //public async Task<IEnumerable<dynamic>> GetItemsSQL(dynamic ctx, APISend values)
        //{
        //    string _entity = values.p_entity;
        //    string _entity_sql = values.p_entity_sql;
        //    string _sqlWhere = values.p_sqlWhere;
        //    string _sqlOrder = values.p_sqlOrder;

        //    string sql;
        //    if (string.IsNullOrEmpty(_entity_sql) == true) { _entity_sql = _entity; };

        //    sql = "select * from " + _entity_sql + " where " + _sqlWhere + _sqlOrder;

        //    var dbSet = DbContextExtensions.exSet(ctx, _entity);
        //    var a = dbSet.FromSql(sql);


        //    return a;
        //}

    }
}
