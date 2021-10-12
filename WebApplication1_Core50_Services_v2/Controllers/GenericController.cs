
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1_Core50.Models;
using WebApplication1_Core50.Services;

namespace GenericCrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        IGenericService _genericService;
        public GenericController(IGenericService service)
        {
            _genericService = service;
        }


        [HttpGet]
        [Route("[action]/entName")]
        public IActionResult GetAllItems(string entName)
        {
            try
            {
                var employees = _genericService.GetItemsList(entName);
                if (employees == null)
                    return NotFound();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get employee details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetItemsById(string entName, int id)
        {
            try
            {
                var employees = _genericService.GetItemById(entName, id);
                if (employees == null)
                    return NotFound();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ItemsRetrieveSQL")]
        [HttpPost]
        public IActionResult ItemsRetrieveSQL(APISendSql values)
        {
        
            try
            {
                var entities = _genericService.GetItemsSql(values.p_sqlTable);
            return Ok(entities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// save employee
        /// </summary>
        /// <param name="employeeModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SaveItems(APISend dto)
        {
            try
            {
                var model = await _genericService.UpdateItemAsync(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete employee  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteItem([FromBody] APISend dto)
        {
            try
            {
                var model = await _genericService.DeleteItemAsync(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
