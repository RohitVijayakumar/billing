using Billing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Service.Interfaces
{
    public interface IService
    {
        (string output, bool issuccess) GetBalance(InputData input);
    }
}
