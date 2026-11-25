using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAggregate;


namespace Shop.Infrastructure.Persistent.Ef.OrderAggregate
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "order");


 
            builder.OwnsOne(o => o.Discount, option =>
            {
                option.Property(o => o.DiscountTitle).HasMaxLength(100).IsRequired();
            });

            builder.OwnsOne(o => o.ShippingMethod, option =>
            {
                option.Property(o => o.ShippingType).HasMaxLength(100).IsRequired(false);
            });


            builder.OwnsMany(o => o.Items, option =>
            {

                option.ToTable("Items", "order");
                option.HasKey(b => b.Id);

            });

            builder.OwnsOne(o => o.Address, option =>
            {
        
                option.ToTable("Addresses", "order");
                
                option.Property(o => o.Province).HasMaxLength(50).IsRequired();
                option.Property(o => o.City).HasMaxLength(50).IsRequired();
                option.Property(o => o.PostalAddress).HasMaxLength(50).IsRequired();
                option.Property(o => o.PostalCode).HasMaxLength(50).IsRequired();
                option.Property(o => o.PhoneNumber).HasMaxLength(50).IsRequired();
                option.Property(o => o.NationalCode).HasMaxLength(50).IsRequired();
                option.Property(o => o.Name).HasMaxLength(100).IsRequired();
                option.Property(o => o.Family).HasMaxLength(100).IsRequired();
            });
        }

    }

} // End Class
