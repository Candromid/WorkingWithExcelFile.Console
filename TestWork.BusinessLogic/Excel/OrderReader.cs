using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Core.Data;
using TestWork.Core;

namespace TestWork.BusinessLogic.Excel
{
    public class OrderReader
    {
        private XLWorkbook workBook;


        public void LoadExcelFile(string filePath)
        {
            workBook = new XLWorkbook(filePath);
        }

        public List<Order> ReadOrders()
        {
            var orders = new List<Order>();

            var workSheet = workBook.Worksheet("Заявки"); // Предполагается, что данные о заказах находятся на листе "Заявки".

            foreach (var row in workSheet.RowsUsed().Skip(1))
            {
                var order = new Order
                {
                    OrderCode = int.Parse(row.Cell(1).Value.ToString()),
                    ProductCode = int.Parse(row.Cell(2).Value.ToString()),
                    ClientCode = int.Parse(row.Cell(3).Value.ToString()),
                    OrderNumber = int.Parse(row.Cell(4).Value.ToString()),
                    Quantity = int.Parse(row.Cell(5).Value.ToString()),
                    OrderDate = DateTime.Parse(row.Cell(6).Value.ToString())
                };

                orders.Add(order);
            }

            return orders;
        }

     
    }
}
