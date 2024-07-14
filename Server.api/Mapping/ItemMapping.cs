using Server.api.Contract;
using Server.api.Entities;

namespace Server.api.Mapping;

public static class ItemMapping {

  public static Item ToEntity(this CreateItem item) {
    return new Item() {
        Id = Guid.NewGuid().ToString(),
        Name = item.Name,
        CategoryId = item.CategoryId,
        Price = item.Price,
        DateAdd = DateTime.Now,
    };
  }

  public static Item ToEntity(this UpdateItem item, string id) {
    return new Item() {
        Id = id,
        Name = item.Name,
        CategoryId = item.CategoryId,
        Price = item.Price,
        DateAdd = DateTime.Now,
    };
  }

  public static ItemSummary ToItemSummaryContract(this Item item) {
    return new(
      item.Id,
      item.Name,
      item.Category!.Name,
      item.Price,
      item.DateAdd
    );
  }

    public static ItemSummary ToItemDetailsContract(this Item item) {
    return new(
      item.Id,
      item.Name,
      item.CategoryId,
      item.Price,
      item.DateAdd
    );
  }

}
