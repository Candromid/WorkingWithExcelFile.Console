using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork.Core.Data;

namespace TestWork.BusinessLogic.Excel
{
    public class ClientReader
    {
        private XLWorkbook workBook;

        public void LoadExcelFile(string filePath)
        {
            workBook = new XLWorkbook(filePath);
        }

        public List<Client> ReadClients()
        {
            var clients = new List<Client>();
            var workSheet = workBook.Worksheet("Клиенты");

            foreach (var row in workSheet.RowsUsed().Skip(1))
            {
                var client = new Client
                {
                    ClientCode = int.Parse(row.Cell(1).Value.ToString()),
                    OrganizationName = row.Cell(2).Value.ToString(),
                    Address = row.Cell(3).Value.ToString(),
                    ContactPersonFullName = row.Cell(4).Value.ToString()
                };

                clients.Add(client);
            }

            return clients;
        }

        //для 3 запроса
        public bool UpdateContactPerson(string organizationName, string newContactPersonFullName)
        {
            var workSheet = workBook.Worksheet("Клиенты");

            var clientRow = workSheet.RowsUsed().FirstOrDefault(row => row.Cell(2).Value.ToString().Equals(organizationName, StringComparison.OrdinalIgnoreCase));

            if (clientRow != null)
            {
                clientRow.Cell(4).Value = newContactPersonFullName;
                workBook.Save();
                return true;
            }

            return false;
        }

    }
}
