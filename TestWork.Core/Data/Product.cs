using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork.Core.Data
{
    public class Product
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
