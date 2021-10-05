using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Service.InputDTO
{
    public class InputData
    {
        [Required]
        public double CustomerAmount { get; set; }
        [Required]
        public double ProductPrice { get; set; }
    }
}
