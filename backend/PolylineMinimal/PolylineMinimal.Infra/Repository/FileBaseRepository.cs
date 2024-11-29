using CsvHelper.Configuration;
using CsvHelper;
using PolylineMinimal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolylineMinimal.Infra.Repository
{
    public abstract class FileBaseRepository
    {
        protected readonly string _basePath = "../../../";

        protected async Task<List<Coordinate>> ReadCoordinatesFromFileAsync(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false,
                MissingFieldFound = null
            });

            var records = new List<Coordinate>();

            await foreach (var record in csv.GetRecordsAsync<RawCoordinate>())
            {

                if (record.X != null && record.Y != null) records.Add(new Coordinate(record.X.Value, record.Y.Value));       

            }

            return records;
        }
    }
}
