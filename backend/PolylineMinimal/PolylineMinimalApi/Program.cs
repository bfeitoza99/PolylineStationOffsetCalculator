using PolylineMinimal.Application.Service;
using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Interfaces.Service;
using PolylineMinimal.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddTransient<IPointFileRepository, PointFileRepository>();
builder.Services.AddTransient<IPolylineFileRepository, PolylineFileRepository>();

builder.Services.AddTransient<IOffSetStationService, OffSetStationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.MapGet("/polyline", async (IPolylineFileRepository service) =>
{
    var results = await service.ReadPolylineAsync();
    return Results.Ok(results);
});

app.MapGet("/points", async (IPointFileRepository service) =>
{
    var results = await service.ReadPointsAsync();
    return Results.Ok(results);
});


app.MapGet("/process", async (IOffSetStationService service) =>
{
    var results = await service.ProcessFilesAsync();
    return Results.Ok(results);
});

app.Run();