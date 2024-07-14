using Server.api.Data;
using Server.api.Endpoints;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connString = builder.Configuration.GetConnectionString("ItemStore");
        builder.Services.AddSqlite<ItemStoreContext>(connString);

        var app = builder.Build();

        app.MapItemEndpoints();
        await app.MigrateDb();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}