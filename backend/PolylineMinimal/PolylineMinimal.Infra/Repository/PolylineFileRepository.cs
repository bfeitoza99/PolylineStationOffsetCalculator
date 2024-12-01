using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolylineMinimal.Infra.Repository
{
    public class PolylineFileRepository : FileBaseRepository ,IPolylineFileRepository
    {
        private readonly string _polylineFile = "polyline.xlsx";

        public  List<Coordinate> ReadPolyline()
        {
            var path = Path.Combine(_basePath, _polylineFile);
            return ReadCoordinatesFromFile(path);
        }
    }
}
