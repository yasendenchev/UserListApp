using UserListApp.Application;
using UserListApp.Infrastructure;
using UserListApp.Server;
using ConfigureWebApiServices = UserListApp.Server.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApiServices();

var app = builder.Build();

app.UseCors(ConfigureWebApiServices.CorsPolicy);

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
