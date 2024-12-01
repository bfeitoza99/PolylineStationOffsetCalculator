using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolylineMinimal.Domain.Models
{
    public class CalculationResult
    {
        public double Offset { get; set; }          
        public double Station { get; set; }        
        public Coordinate ClosestPoint { get; set; } 
        public Coordinate CurrentPoint { get; set; }

        public CalculationResult(double offset, double station, Coordinate closestPoint, Coordinate currentPoint)
        {
            Offset = offset;
            Station = station;
            ClosestPoint = closestPoint;
            CurrentPoint = currentPoint;
        }      
    }
}
