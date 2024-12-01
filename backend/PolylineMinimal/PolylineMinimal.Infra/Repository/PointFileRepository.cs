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
        private readonly string _pointsFile = "points.xlsx";
       
        public  List<Coordinate> ReadPoints()
        {
            var path = Path.Combine(_basePath, _pointsFile);
            return ReadCoordinatesFromFile(path);
        }
       
    }
}
