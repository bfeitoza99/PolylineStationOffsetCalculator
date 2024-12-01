using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Interfaces.Service;
using PolylineMinimal.Domain.Models;
using System;

namespace PolylineMinimal.Application.Service
{
    public class OffSetStationService : IOffSetStationService
    {
        private readonly IPolylineFileRepository _polylineRepository;
        private readonly IPointFileRepository _pointRepository;

        public OffSetStationService(IPolylineFileRepository polylineRepository, IPointFileRepository pointRepository)
        {
            _polylineRepository = polylineRepository;
            _pointRepository = pointRepository;
        }

        public List<CalculationResult> ProcessFiles()
        {
            var polyline =  _polylineRepository.ReadPolyline();
            var points =  _pointRepository.ReadPoints();

            if (!ValidateInput(polyline, points))
            {               
                return new List<CalculationResult>();
            }

            return points.Select(point => CalculateOffsetAndStation(point, polyline)).Where(result => result != null).ToList();
        }

        private CalculationResult CalculateOffsetAndStation(Coordinate point, List<Coordinate> polyline)
        {
            double minOffset = double.MaxValue;
            double station = 0;
            double accumulatedDistance = 0;
            Coordinate closestPoint = null;

            for (int i = 0; i < polyline.Count - 1; i++)
            {
                var segmentStart = polyline[i];
                var segmentEnd = polyline[i + 1];

                if (!ValidateSegment(segmentStart, segmentEnd))
                {
                    continue;
                }

                var (distance, projection) = CalculatePerpendicularDistance(segmentStart, segmentEnd, point);

                if (distance < minOffset)
                {
                    minOffset = distance;
                    closestPoint = projection;
                    station = accumulatedDistance + CalculateDistance(segmentStart, projection);
                }

                accumulatedDistance += CalculateDistance(segmentStart, segmentEnd);
            }

            if (closestPoint == null)
            {                
                return null;
            }          

            return new CalculationResult(minOffset, station, closestPoint, point);
        }

        private (double, Coordinate) CalculatePerpendicularDistance(Coordinate segmentStart, Coordinate segmentEnd, Coordinate targetPoint)
        {
            double segmentDeltaX = segmentEnd.X - segmentStart.X;
            double segmentDeltaY = segmentEnd.Y - segmentStart.Y;
            double segmentLengthSquared = segmentDeltaX * segmentDeltaX + segmentDeltaY * segmentDeltaY;

            if (segmentLengthSquared == 0)
            {                
                return (CalculateDistance(segmentStart, targetPoint), segmentStart);
            }

            double projectionScalar = ((targetPoint.X - segmentStart.X) * segmentDeltaX + (targetPoint.Y - segmentStart.Y) * segmentDeltaY) / segmentLengthSquared;
            projectionScalar = Math.Max(0, Math.Min(1, projectionScalar));

            var projectionPoint = new Coordinate(
                segmentStart.X + projectionScalar * segmentDeltaX,
                segmentStart.Y + projectionScalar * segmentDeltaY
            );

            return (CalculateDistance(projectionPoint, targetPoint), projectionPoint);
        }

        private double CalculateDistance(Coordinate a, Coordinate b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private bool ValidateSegment(Coordinate start, Coordinate end)
        {
            if (start.X == end.X && start.Y == end.Y)
            {               
                return false;
            }
            return true;
        }

        private bool ValidateInput(List<Coordinate> polyline, List<Coordinate> points)
        {
            if (polyline == null || polyline.Count < 2 ||  points == null || points.Count == 0)
            {               
                return false;
            }
            return true;
        }
    }
}
