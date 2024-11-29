using PolylineMinimal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolylineMinimal.Domain.Interfaces.Repository
{
    public interface IPointFileRepository
    {        
        Task<List<Coordinate>> ReadPointsAsync();
    }
}
