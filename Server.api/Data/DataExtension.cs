using Microsoft.EntityFrameworkCore;

namespace Server.api.Data;

public static class DataExtension
{
  public static async Task MigrateDb (this WebApplication app) {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ItemStoreContext>();
    await dbContext.Database.MigrateAsync();
  }
}
