using Server.api.Data;
using Server.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ItemStore");
builder.Services.AddSqlite<ItemStoreContext>(connString);

var app = builder.Build();

app.MapItemEndpoints();
app.MigrateDb();
app.MapGet("/", () => "Hello World!");

app.Run();
