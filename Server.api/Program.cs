using Server.api.Contract;
using Server.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapItemEndpoints();
app.MapGet("/", () => "Hello World!");

app.Run();
