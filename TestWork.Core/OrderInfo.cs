using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork.Core
{
    public class OrderInfo
    {
        public string ClientName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
