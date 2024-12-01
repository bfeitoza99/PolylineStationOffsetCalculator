
using Moq;
using PolylineMinimal.Application.Service;
using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Models;

public class OffSetStationServiceTests
{
    private readonly Mock<IPolylineFileRepository> _polylineRepositoryMock;
    private readonly Mock<IPointFileRepository> _pointRepositoryMock;
    private readonly OffSetStationService _service;

    public OffSetStationServiceTests()
    {
        _polylineRepositoryMock = new Mock<IPolylineFileRepository>();
        _pointRepositoryMock = new Mock<IPointFileRepository>();
        _service = new OffSetStationService(_polylineRepositoryMock.Object, _pointRepositoryMock.Object);
    }

    [Fact]
    public void ProcessFiles_ReturnsEmptyList_WhenInputIsInvalid()
    {
       
        _polylineRepositoryMock.Setup(repo => repo.ReadPolyline()).Returns(new List<Coordinate>());
        _pointRepositoryMock.Setup(repo => repo.ReadPoints()).Returns(new List<Coordinate>());
        
        var results = _service.ProcessFiles();

        Assert.Empty(results);
    }

    [Fact]
    public void ProcessFiles_CalculatesCorrectly_ForValidInput()
    {
        // Arrange
        var polyline = new List<Coordinate>
        {
            new Coordinate(0, 0),
            new Coordinate(10, 10),
            new Coordinate(20, 20),
        };

        var points = new List<Coordinate>
        {
            new Coordinate(15, 25),
            new Coordinate(-5, -5),
        };

        _polylineRepositoryMock.Setup(repo => repo.ReadPolyline()).Returns(polyline);
        _pointRepositoryMock.Setup(repo => repo.ReadPoints()).Returns(points);

   
        var results = _service.ProcessFiles();
    
        Assert.Equal(2, results.Count);
       
        Assert.Equal(7.07, results[0].Offset, 2); 
        Assert.Equal(28.28, results[0].Station, 2); 

        Assert.Equal(7.07, results[1].Offset, 2);
        Assert.Equal(0, results[1].Station, 2); 
    }

    [Fact]
    public void CalculateOffsetAndStation_ReturnsNull_WhenNoValidSegmentExists()
    {
        var polyline = new List<Coordinate>
        {
            new Coordinate(0, 0),
            new Coordinate(0, 0), 
        };

        var point = new Coordinate(10, 10);

     
        var result = _service.ProcessFiles();

        Assert.Empty(result);
    }

    [Fact]
    public void ValidateSegment_ReturnsFalse_ForIdenticalPoints()
    {  
        var segmentStart = new Coordinate(5, 5);
        var segmentEnd = new Coordinate(5, 5);

        var result = _service.GetType()
            .GetMethod("ValidateSegment", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(_service, new object[] { segmentStart, segmentEnd });

       
        Assert.False((bool)result);
    }
}
