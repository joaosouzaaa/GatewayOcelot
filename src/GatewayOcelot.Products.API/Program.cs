using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.DependencyInjection;
using GatewayOcelot.Products.API.Filters;
using GatewayOcelot.Products.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers(options => options.Filters.AddService<NotificationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencyInjection(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<UnexpectedErrorMiddleware>();
}

app.UseCors(CorsNamesConstants.CorsPolicy);
app.UseAuthorization();
app.MapControllers();

app.Run();
