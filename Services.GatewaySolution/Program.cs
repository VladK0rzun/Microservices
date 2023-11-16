using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Services.GatewaySolution.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAutherization();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();



app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
