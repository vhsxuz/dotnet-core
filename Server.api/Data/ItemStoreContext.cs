using Microsoft.EntityFrameworkCore;
using Server.api.Entities;

namespace Server.api.Data;

public class ItemStoreContext(DbContextOptions<ItemStoreContext> options)
  : DbContext(options)
{
  public DbSet<Item> Items => Set<Item>();
  public DbSet<Category> Categories => Set<Category>();
}
