using Application.Middleware;
using Core;
using Infrastructure;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services
   .AddInfrastructure(builder.Configuration)
   .AddRepositories()
   .AddServices();

var app = builder.Build();
app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
