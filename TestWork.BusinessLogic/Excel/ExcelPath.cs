using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using TestWork.Core.Data;

namespace TestWork.BusinessLogic.Excel
{
    public class ExcelPath
    {

        //для 1 запроса

        public string GetUserInputFilePath()
        {
            string filePath;
            do
            {
                Console.WriteLine("Введите путь к файлу Excel с данными:");
                filePath = Console.ReadLine();

                if (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Неверный формат файла. Укажите путь к файлу с расширением .xlsx.");
                }
                else if (!System.IO.File.Exists(filePath))
                {
                    Console.WriteLine("Файл не найден. Проверьте правильность пути.");
                }
            } while (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(filePath));

            return filePath;
        }
    }

}
