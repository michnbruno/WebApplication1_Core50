using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_Core50.Contexts;
using WebApplication1_Core50.Models;

namespace WebApplication1_Core50.Services
{
    public interface IGenericService
    {
        dynamic GetItemsList(string entName);

        dynamic GetItemById(string entName, int id);

        dynamic GetItemsSql(string sqlTable);

        Task<dynamic> UpdateItemAsync(APISend dto);

        Task<dynamic> DeleteItemAsync(APISend dto);
   
    }
}
