using Application.Middleware;
using Core;
using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services
   .AddInfrastructure(builder.Configuration)
   .AddRepositories()
   .AddServices()
   .AddApiServices(builder.Configuration)
   .AddSwaggerServices();

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
