using Microsoft.EntityFrameworkCore;

namespace Board.Models
{
  public class BoardContext : DbContext
  {
    public DbSet<Category> Topics { get; set; }
    public DbSet<Item> Posts { get; set; }
    public DbSet<CategoryItem> TopicPost { get; set; }

    public BoardContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}