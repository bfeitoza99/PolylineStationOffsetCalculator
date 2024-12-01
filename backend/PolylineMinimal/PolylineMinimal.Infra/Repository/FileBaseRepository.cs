using OfficeOpenXml; // Biblioteca EPPlus
using PolylineMinimal.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PolylineMinimal.Infra.Repository
{
    public abstract class FileBaseRepository
    {
        protected readonly string _basePath;

        public FileBaseRepository()
        {          
            _basePath = Environment.GetEnvironmentVariable("BASE_PATH") ?? "./";
        }

        protected List<Coordinate> ReadCoordinatesFromFile(string path)
        {

            var records = new List<Coordinate>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(path));
            var worksheet = package.Workbook.Worksheets[0];

            for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
            {

                var values = worksheet.Cells[row, 1].Value.ToString();

                if (string.IsNullOrEmpty(values))
                    continue;

                var splittedValues = values.Split(",");

                if (splittedValues.Length < 2)
                    continue;

                var xValue = splittedValues[0];
                var yValue = splittedValues[1];

                if (xValue != null && yValue != null)
                {
                    if (double.TryParse(xValue.ToString(), out var x) &&
                        double.TryParse(yValue.ToString(), out var y))
                    {
                        records.Add(new Coordinate(x, y));
                    }
                }
            }

            return records;

        }
    }
}
