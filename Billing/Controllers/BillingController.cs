using Billing.Service.InputDTO;
using Billing.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        public readonly IService _service;

        public class ResultModel
        {
            public string output { get; set; }
            public string success { get; set; }
        }
        public BillingController(IService service)
        {
            _service = service;
        }

        [HttpGet("GetBalance")]
        public ResultModel GetBalance([FromQuery]InputData input)
        {
            var result = _service.GetBalance(input);
            var response = new ResultModel()
            {
                output = result.output,
                success = result.issuccess ? "S_200" : "EX_200"
            };
            return response;
        }

        [HttpGet("GetBalanceinFormatedtext")]
        public ContentResult GetBalanceinFormatedtext([FromQuery] InputData input)
        {
            var result = _service.GetBalance(input);
            return Content(result.output);
        }
    }
}
