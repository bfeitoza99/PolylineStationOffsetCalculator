using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Interfaces.Service;
using PolylineMinimal.Domain.Models;

namespace PolylineMinimal.Application.Service
{
    public  class OffSetStationService : IOffSetStationService
    {

        private readonly IPolylineFileRepository _polylineRepository;
        private readonly IPointFileRepository _pointRepository;

        public OffSetStationService(IPolylineFileRepository polylineRepository, IPointFileRepository pointRepository)
        {
            _polylineRepository = polylineRepository;
            _pointRepository = pointRepository;
        }

        public async Task<List<CalculationResult>> ProcessFilesAsync()
        {
            var polyline = await _polylineRepository.ReadPolylineAsync();
            var points = await _pointRepository.ReadPointsAsync();


            return points.Select(point => CalculateOffsetAndStation(point, polyline)).ToList();
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

                var (distance, projection) = CalculatePerpendicularDistance(segmentStart, segmentEnd, point);

                if (distance < minOffset)
                {
                    minOffset = distance;
                    closestPoint = projection;
                    station = accumulatedDistance + CalculateDistance(segmentStart, projection);
                }

                accumulatedDistance += CalculateDistance(segmentStart, segmentEnd);
            }

            return new CalculationResult(minOffset, station, closestPoint);
         
        }

        private (double, Coordinate) CalculatePerpendicularDistance(Coordinate start, Coordinate end, Coordinate point)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;
            double lengthSquared = dx * dx + dy * dy;

            if (lengthSquared == 0) return (CalculateDistance(start, point), start);

            double t = ((point.X - start.X) * dx + (point.Y - start.Y) * dy) / lengthSquared;
            t = Math.Max(0, Math.Min(1, t));

            var projection = new Coordinate(
                start.X + t * dx,
                start.Y + t * dy
            );

            return (CalculateDistance(projection, point), projection);
        }

        private double CalculateDistance(Coordinate a, Coordinate b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
