using PolylineMinimal.Application.Service;
using PolylineMinimal.Domain.Interfaces.Repository;
using PolylineMinimal.Domain.Interfaces.Service;
using PolylineMinimal.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() 
               .AllowAnyMethod() 
               .AllowAnyHeader(); 
    });
});

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

app.UseCors("AllowAllOrigins");
app.MapGet("/polyline",  (IPolylineFileRepository service) =>
{
    var results =  service.ReadPolyline();
    return Results.Ok(results);
});

app.MapGet("/points",  (IPointFileRepository service) =>
{
    var results =  service.ReadPoints();
    return Results.Ok(results);
});


app.MapGet("/process",  (IOffSetStationService service) =>
{
    var results =  service.ProcessFiles();
    return Results.Ok(results);
});

app.Run();