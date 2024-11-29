using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolylineMinimal.Infra.Repository
{
    public class PointFileRepository: FileBaseRepository, IPointFileRepository
    {
        private readonly string _pointsFile = "points.csv";
       
        public async Task<List<Coordinate>> ReadPointsAsync()
        {
            var path = Path.Combine(_basePath, _pointsFile);
            return await ReadCoordinatesFromFileAsync(path);
        }
       
    }
}
