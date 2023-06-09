using Calendario.Config;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();

services.AddSwaggerGen();
services.AddEndpointsApiExplorer();

services.ConfigureIoC();

services.AddHttpClient();

services.ConfigureApiDocumentationUI();

services.ConfigureCalendarService(config.GetSection("web"));

var app = builder.Build();

app.ConfigureApiDocumentationUI();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();
