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
        private readonly string _polylineFile = "polyline.csv";

        public async Task<List<Coordinate>> ReadPolylineAsync()
        {
            var path = Path.Combine(_basePath, _polylineFile);
            return await ReadCoordinatesFromFileAsync(path);
        }
    }
}
