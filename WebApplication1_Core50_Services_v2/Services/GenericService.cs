using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1_Core50.Contexts;
using WebApplication1_Core50.Models;

namespace WebApplication1_Core50.Services
{
    public class GenericService : IGenericService
    {

        private ApplicationDbContext _context;



        public GenericService(ApplicationDbContext context)
        {
            _context = context;

        }

        public dynamic GetItemsList(string entName)
        {
            dynamic itemsList;
            try
            {
                var ctxType = _context.GetType();
                dynamic entities = ctxType.GetProperty(entName).GetValue(_context, null);
                itemsList = entities;
                // return itemsList;
            }
            catch (Exception ex)
            {
                var test = ex;
                throw new Exception(ex.Message + Environment.NewLine + "no entity with name of " + entName);
            }
            return itemsList;
        }
        public dynamic GetItemById(string entName, int id)
        {
            dynamic item;
            try
            {

                var ctxType = _context.GetType();
                dynamic entities = ctxType.GetProperty(entName).GetValue(_context, null);
                var entityDbSetName = entities.EntityType.ClrType.Name;
                var entityPrimaryKeyType = DbContextExtensions.GetPrimaryKeyType(_context, entityDbSetName);
                var typedRecNum = DbContextExtensions.ConvertKeyType(id, entityPrimaryKeyType);
                var record = entities.Find(typedRecNum);

                item = record;
         
            }
            catch (Exception ex)
            {
                var test = ex;
                throw new Exception(ex.Message + Environment.NewLine + "no entity with name of " + entName + " and id = " + id);
            }
            return item;
        }
        public dynamic GetItemsSql(string sqlTable)
        {
            dynamic ret;
                try
            {
            using (var dbContext = _context)
            {
                //  var dynamicTable = dbContext.Set<dynamic>(tableName);

                using (var command = dbContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "Select * from " + sqlTable;

                    dbContext.Database.OpenConnection();



                    SqlDataReader reader = (SqlDataReader)command.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    List<dynamic> myObjects = new List<dynamic>();
                    if (dataTable.Rows.Count > 0)
                    {
                        var serializedMyObjects = JsonConvert.SerializeObject(dataTable);
                        // Here you get the object
                        myObjects = (List<dynamic>)JsonConvert.DeserializeObject(serializedMyObjects, typeof(List<dynamic>));
                        ret = myObjects;
                    }
                    else
                    {
                        return null;
                    }



                }

            }
            }
            catch (Exception ex)
            {
                var test = ex;
                throw new Exception(ex.Message + Environment.NewLine + "error retrieving sql");
            }
            return ret;
        }
        public async Task<dynamic> UpdateItemAsync(APISend dto)
        {
            dynamic updateRecReturn;
            try
            {
                // get collection of entities
                var ctxType = _context.GetType();
            dynamic entities = (ctxType.GetProperty(dto.p_entity).GetValue(_context, null));
 
            //get dbset entity name (pluralization)
            var assembly = Assembly.GetExecutingAssembly();
            var entityDbSetName = entities.EntityType.ClrType.Name;

            //get new strongly typed record stub
            var entityType = DbContextExtensions.GetEntityType(assembly, entityDbSetName);
            var entityStub = Activator.CreateInstance(entityType, true);

            //map update record to typed stub
            dynamic typedEntityRecord = DbContextExtensions.GetUpdateAddObject(dto.record, entityStub);

            //primary key info
            var primaryKeyName = DbContextExtensions.GetPrimaryKeyName(_context, entityDbSetName);
            var primaryKeyRecordValue = DbContextExtensions.GetPrimaryKeyRecordValue(typedEntityRecord, primaryKeyName);


            // DO SAVE

            
            if (primaryKeyRecordValue == 0)
            {
                updateRecReturn = entities.Add(typedEntityRecord);

            }
            else
            {
                updateRecReturn = entities.Update(typedEntityRecord);
            }

            var resp = await this._context.SaveChanges().ConfigureAwait(true);
            this._context.Dispose();
            }
            catch (Exception ex)
            {
                var test = ex;
                throw new Exception(ex.Message + Environment.NewLine + "error saving/updating record.");
            }

            var e = updateRecReturn.Entity;
            return e;
     
        }
        public async Task<dynamic> DeleteItemAsync(APISend dto)
        {
            dynamic updateRecReturn;
            try
            {
                // get collection of entities
                var ctxType = _context.GetType();
            dynamic entities = (ctxType.GetProperty(dto.p_entity).GetValue(_context, null));

            //get dbset entity name (pluralization)
            var assembly = Assembly.GetExecutingAssembly();
            var entityDbSetName = entities.EntityType.ClrType.Name;

            //get new strongly typed record stub
            var entityType = DbContextExtensions.GetEntityType(assembly, entityDbSetName);
            var entityStub = Activator.CreateInstance(entityType, true);

            //map update record to typed stub
            dynamic typedEntityRecord = DbContextExtensions.GetUpdateAddObject(dto.record, entityStub);

           // dynamic updateRecReturn;
            updateRecReturn = entities.Remove(typedEntityRecord);


            var resp = await this._context.SaveChanges().ConfigureAwait(true);
            this._context.Dispose();

        }
            catch (Exception ex)
            {
                var test = ex;
                throw new Exception(ex.Message + Environment.NewLine + "error deleting record");
            }

    var e = updateRecReturn.Entity;
            return e;
        }


        // PRIVATE METHODS


        private static string ConcatenateSql(string entity_sql, string sqlWhere, string sqlOrderBy)
        {


            string sql;
            //  if (string.IsNullOrEmpty(entity_sql) == true) { entity_sql = entity; };

            sql = "select * from " + entity_sql; // + " where " + sqlWhere + sqlOrder;

            return sql;
            //  var sql2 = "Select * from " + entity_sql;
        }


    }
}
