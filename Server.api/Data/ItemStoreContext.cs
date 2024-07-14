using Microsoft.EntityFrameworkCore;
using Server.api.Entities;

namespace Server.api.Data;

public class ItemStoreContext(DbContextOptions<ItemStoreContext> options)
  : DbContext(options)
{
  public DbSet<Item> Items => Set<Item>();
  public DbSet<Category> Categories => Set<Category>();
  
  override protected void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Category>().HasData( 
      new { Id= Guid.NewGuid().ToString(), Name= "Stationary"},
      new { Id= Guid.NewGuid().ToString(), Name= "Toys"},
      new { Id= Guid.NewGuid().ToString(), Name= "Books"}
     );
  }
}
