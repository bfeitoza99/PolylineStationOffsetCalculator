using PolylineMinimal.Domain.Models;


namespace PolylineMinimal.Domain.Interfaces.Service
{
    public interface IOffSetStationService
    {
        List<CalculationResult> ProcessFiles();
    }
}
