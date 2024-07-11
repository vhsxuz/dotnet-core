using Server.api.Contract;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var date = DateTime.Now;

const string itemEndpointName = "GetItem";

List<Item> items = [
    new Item("b539d123-2c31-4049-a940-c8a666437f37", "Book", "stationary", 3.5M, date),
    new Item(Guid.NewGuid().ToString(), "Pencil", "stationary", 1.25M, date),
    new Item(Guid.NewGuid().ToString(), "Eraser", "stationary", 1, date),
];

app.MapGet("api/v1/items", () => items);

app.MapGet("api/v1/items/{id}", (string id) => {
  Item? foundItems = items.Find(item => item.Id == id);
  return foundItems is null ? Results.NotFound() : Results.Ok(foundItems);

}).WithName(itemEndpointName);

app.MapPost("api/v1/items", (CreateItem newItem) => {
  Item item = new (
    Guid.NewGuid().ToString(),
    newItem.Name,
    newItem.Category,
    newItem.Price,
    date);

  items.Add(item);

  return Results.CreatedAtRoute(itemEndpointName, new {id = item.Id}, item);

});

app.MapPut("/api/v1/items/{id}", (string id, UpdateItem item) => {
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

app.MapDelete("api/v1/items/{id}", (string id) => {
  items.RemoveAll(item => item.Id == id);
  return Results.NoContent();
});

app.MapGet("/", () => "Hello World!");

app.Run();
