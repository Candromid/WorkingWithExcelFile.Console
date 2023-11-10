using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork.Core.Data
{
    public class Order
    {
        public int OrderCode { get; set; }
        public int ProductCode { get; set; }
        public int ClientCode { get; set; }
        public int OrderNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
