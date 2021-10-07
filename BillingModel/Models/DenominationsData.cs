using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Models
{
    public class DenominationsData
    {
        public List<int> PoundValue { get; set; }
        public List<int> PenceValue { get; set; }
        public string SymbolValue { get; set; }
    }
}
