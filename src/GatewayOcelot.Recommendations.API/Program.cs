using GatewayOcelot.Recommendations.API.Constants;
using GatewayOcelot.Recommendations.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsNamesConstants.CorsPolicy);
app.UseAuthorization();
app.MapControllers();
app.MigrateDatabase();

app.Run();
