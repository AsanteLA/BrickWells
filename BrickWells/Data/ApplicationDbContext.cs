using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrickWells.Models;

namespace BrickWells.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    
}

public partial class BrickContext : DbContext
{
    public BrickContext(DbContextOptions<BrickContext> options)
        : base(options)
    {
    }

    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ItemBasedRec> ItemBasedRecs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity
                .HasKey(e => e.CustomerId);
                entity.ToTable("customers");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CountryOfResidence).HasColumnName("country_of_residence");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            
            entity.HasNoKey();


            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasKey(e => e.TransactionId);
                entity.ToTable("orders");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Bank).HasColumnName("bank");
            entity.Property(e => e.CountryOfTransaction).HasColumnName("country_of_transaction");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.EntryMode).HasColumnName("entry_mode");
            entity.Property(e => e.Fraud).HasColumnName("fraud");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
            entity.Property(e => e.TypeOfCard).HasColumnName("type_of_card");
            entity.Property(e => e.TypeOfTransaction).HasColumnName("type_of_transaction");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasKey(e => e.ProductId);
                entity.ToTable("products");

            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImgLink).HasColumnName("img_link");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NumParts).HasColumnName("num_parts");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PrimaryColor).HasColumnName("primary_color");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.SecondaryColor).HasColumnName("secondary_color");
            entity.Property(e => e.Year).HasColumnName("year");
        });
        
        modelBuilder.Entity<ItemBasedRec>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("item_based_recs");

            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.FirstRec).HasColumnName("first_rec");
            entity.Property(e => e.SecondRec).HasColumnName("second_rec");
            entity.Property(e => e.ThirdRec).HasColumnName("third_rec");
            entity.Property(e => e.FourthRec).HasColumnName("fourth_rec");
            entity.Property(e => e.FifthRec).HasColumnName("fifth_rec");
            entity.Property(e => e.SixthRec).HasColumnName("sixth_rec");
            entity.Property(e => e.SeventhRec).HasColumnName("seventh_rec");
            entity.Property(e => e.EighthRec).HasColumnName("eighth_rec");
            entity.Property(e => e.NinthRec).HasColumnName("ninth_rec");
            entity.Property(e => e.TenthRec).HasColumnName("tenth_rec");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    
}
