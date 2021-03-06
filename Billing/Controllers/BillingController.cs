using Billing.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Billing.Models;

namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        public readonly IService _service;
        public BillingController(IService service)
        {
            _service = service;
        }

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"> Input details - customer amount and prduct price</param>
        /// <returns>Balance denominations or any error messages</returns>
        [HttpGet("GetBalance")]
        public ResultModel GetBalance([FromQuery] InputData input)
        {
            var result = _service.GetBalance(input);
            var response = new ResultModel()
            {
                Output = result.output,
                Success = result.issuccess ? "S_200" : "EX_200"
            };
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">Input details - customer amount and prduct price</param>
        /// <returns>Balance denominations or any error messages</returns>
        [HttpGet("GetBalanceinFormatedtext")]
        public ContentResult GetBalanceinFormatedtext([FromQuery] InputData input)
        {
            var result = _service.GetBalance(input);
            return Content(result.output);
        }

        #endregion
    }
}
