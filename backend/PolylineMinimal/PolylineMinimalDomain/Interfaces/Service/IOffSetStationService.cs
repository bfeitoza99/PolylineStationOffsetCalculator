using PolylineMinimal.Domain.Models;


namespace PolylineMinimal.Domain.Interfaces.Service
{
    public interface IOffSetStationService
    {
        Task<List<CalculationResult>> ProcessFilesAsync();
    }
}
