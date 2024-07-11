using Server.api.Contract;

namespace Server.api.Endpoints;

public static class ItemEndpoints {
  private static readonly List<Item> items = [
    new Item("b539d123-2c31-4049-a940-c8a666437f37", "Book", "stationary", 3.5M, DateTime.Now),
    new Item(Guid.NewGuid().ToString(), "Pencil", "stationary", 1.25M, DateTime.Now),
    new Item(Guid.NewGuid().ToString(), "Eraser", "stationary", 1, DateTime.Now),
  ];

  public static RouteGroupBuilder MapItemEndpoints(this WebApplication app) {
    DateTime date = DateTime.Now;
    const string itemEndpointName = "GetItem";

    var group = app.MapGroup("api/v1/items");

    group.MapGet("/", () => items);

    group.MapGet("/{id}", (string id) => {
      Item? foundItems = items.Find(item => item.Id == id);
      return foundItems is null ? Results.NotFound() : Results.Ok(foundItems);
    }).WithName(itemEndpointName);

    group.MapPost("/", (CreateItem newItem) => {
      Item item = new (
        Guid.NewGuid().ToString(),
        newItem.Name,
        newItem.Category,
        newItem.Price,
        date);
      items.Add(item);
      return Results.CreatedAtRoute(itemEndpointName, new {id = item.Id}, item);
    }).WithParameterValidation();

    group.MapPut("/{id}", (string id, UpdateItem item) => {
      var index = items.FindIndex(item => item.Id == id);

      if (index == -1) { return Results.NotFound(); }

      items[index] = new Item(
        id,
        item.Name,
        item.Category,
        item.Price,
        date);

      return Results.NoContent();  
    });

    group.MapDelete("/{id}", (string id) => {
      int removeCount = items.RemoveAll(item => item.Id == id);
      return removeCount == 0 ? Results.NotFound() : Results.NoContent();
    });

    return group;
  }

}
