using Calendario.Config;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.ConfigureIoC();

services.AddHttpClient();

services.ConfigureApiDocumentationUI();

services.ConfigureCalendarService(config.GetSection("web"));

var app = builder.Build();

app.ConfigureApiDocumentationUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();
