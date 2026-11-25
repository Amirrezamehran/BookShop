using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Shop.Domain.CategoryAggregate;
using Shop.Domain.CommentAggregate;
using Shop.Domain.OrderAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Domain.RoleAggregate;
using Shop.Domain.SellerAggregate;
using Shop.Domain.SiteEntities;
using Shop.Domain.UserAggregate;


namespace Shop.Infrastructure.Persistent.Ef
{
    public class ShopContext : DbContext
    {
      

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerInventory> Inventories { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
  
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }

} // End Class
