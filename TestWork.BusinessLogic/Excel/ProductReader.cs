using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Core.Data;

namespace TestWork.BusinessLogic.Excel
{
    public class ProductReader
    {
        private XLWorkbook workBook;

        public void LoadExcelFile(string filePath)
        {
            workBook = new XLWorkbook(filePath);
        }

        public List<Product> ReadProducts()
        {
            var products = new List<Product>();
            var workSheet = workBook.Worksheet("Товары");

            foreach (var row in workSheet.RowsUsed().Skip(1))
            {
                var product = new Product
                {
                    ProductCode = int.Parse(row.Cell(1).Value.ToString()),
                    ProductName = row.Cell(2).Value.ToString(),
                    UnitOfMeasurement = row.Cell(3).Value.ToString(),
                    UnitPrice = decimal.Parse(row.Cell(4).Value.ToString())
                };

                products.Add(product);
            }

            return products;
        }

    }
}
