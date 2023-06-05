using Calendario.Servico;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar Google", Version = "v1" });
});
services.AddHttpClient();

services.AddScoped<GoogleCalendarAuthorization>();
services.AddScoped<GoogleCalendarService>();
services.AddScoped(_ => "Calendario");

#region [Cors]
services.AddCors();
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.UseRouting();

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyOrigin();
    c.AllowAnyMethod();        
    c.AllowAnyHeader();
});
#endregion

app.UseAuthorization();

app.MapControllers();


app.Run();
