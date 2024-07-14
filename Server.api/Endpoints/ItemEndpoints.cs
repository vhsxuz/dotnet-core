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

    group.MapGet("/", (ItemStoreContext dbContext) => dbContext.Items
      .Include(item => item.Category)
      .Select(item => item.ToItemSummaryContract())
      .AsNoTracking());

    group.MapGet("/{id}", (string id, ItemStoreContext dbContext) => {
      Item ? foundItems = dbContext.Items.Find(id);
      return foundItems is null ? Results.NotFound() : Results.Ok(foundItems.ToItemDetailsContract());
    }).WithName(itemEndpointName);

    group.MapPost("/", (CreateItem newItem, ItemStoreContext dbContext) => {

      var foundCategory = dbContext.Categories.Find(newItem.CategoryId);
      if (foundCategory == null) {
          return Results.NotFound(new { Message = "Category not found." });
      }

      Item item = newItem.ToEntity();

      dbContext.Add(item);
      dbContext.SaveChanges();

      
      return Results.CreatedAtRoute(
        itemEndpointName, 
        new {id = item.Id}, 
        item.ToItemSummaryContract());

    });

    group.MapPut("/{id}", (string id, UpdateItem updateItem, ItemStoreContext dbContext) => {
      
      var existingItem = dbContext.Items.Find(id);

      if (existingItem is null) { return Results.NotFound(); }

      dbContext.Entry(existingItem)
        .CurrentValues
        .SetValues(updateItem.ToEntity(id));
      
      dbContext.SaveChanges();

      return Results.NoContent();  
    });

    group.MapDelete("/{id}", (string id, ItemStoreContext dbContext) => {
      var itemToDelete = dbContext.Items.
        Include(i => i.Category).
        FirstOrDefault(item => item.Id == id);
      
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
