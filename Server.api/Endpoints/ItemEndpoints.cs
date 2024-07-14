using Server.api.Contract;
using Server.api.Entities;
using Server.api.Data;
using Server.api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Server.api.Endpoints;

public static class ItemEndpoints {
  private static readonly List<ItemSummary> items = [
    new ItemSummary("b539d123-2c31-4049-a940-c8a666437f37", "Book", "stationary", 3.5M, DateTime.Now),
    new ItemSummary(Guid.NewGuid().ToString(), "Pencil", "stationary", 1.25M, DateTime.Now),
    new ItemSummary(Guid.NewGuid().ToString(), "Eraser", "stationary", 1, DateTime.Now),
  ];

  public static RouteGroupBuilder MapItemEndpoints(this WebApplication app) {
    DateTime date = DateTime.Now;
    const string itemEndpointName = "GetItem";

    var group = app.MapGroup("api/v1/items");

    group.MapGet("/", async (ItemStoreContext dbContext) => await dbContext.Items
      .Include(item => item.Category)
      .Select(item => item.ToItemSummaryContract())
      .AsNoTracking()
      .ToListAsync());

    group.MapGet("/{id}", async (string id, ItemStoreContext dbContext) =>  {
      Item? foundItems = await dbContext.Items.FindAsync(id);
      return foundItems is null ? Results.NotFound() : Results.Ok(foundItems.ToItemDetailsContract());
    }).WithName(itemEndpointName);

    group.MapPost("/", async (CreateItem newItem, ItemStoreContext dbContext) => {

      var foundCategory = dbContext.Categories.Find(newItem.CategoryId);
      if (foundCategory == null) {
          return Results.NotFound(new { Message = "Category not found." });
      }

      Item item = newItem.ToEntity();

      dbContext.Add(item);
      await dbContext.SaveChangesAsync();

      
      return Results.CreatedAtRoute(
        itemEndpointName, 
        new {id = item.Id}, 
        item.ToItemSummaryContract());

    });

    group.MapPut("/{id}", async (string id, UpdateItem updateItem, ItemStoreContext dbContext) => {
      
      var existingItem = await dbContext.Items.FindAsync(id);

      if (existingItem is null) { return Results.NotFound(); }

      dbContext.Entry(existingItem)
        .CurrentValues
        .SetValues(updateItem.ToEntity(id));
      
      await dbContext.SaveChangesAsync();

      return Results.NoContent();  
    });

    group.MapDelete("/{id}", async (string id, ItemStoreContext dbContext) => {
      var itemToDelete = await dbContext.Items.
        Include(i => i.Category).
        FirstOrDefaultAsync(item => item.Id == id);
      
      if (itemToDelete == null) {
          return Results.NotFound();
      }

      dbContext.Items.Remove(itemToDelete);
      dbContext.SaveChanges();

      var deletedItem = itemToDelete.ToItemDetailsContract();

      return Results.Ok(deletedItem);
    });

    return group;
  }

}
