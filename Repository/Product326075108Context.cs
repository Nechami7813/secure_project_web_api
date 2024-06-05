using System;
using System.Collections.Generic;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public partial class Product326075108Context : DbContext
{
    public Product326075108Context()
    {
    }

    public Product326075108Context(DbContextOptions<Product326075108Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=Product_326075108;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B2CD968E7");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order_tb__C3905BCF2B4A389B");

            entity.ToTable("Order");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Order_tbl__UserI__3E52440B");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order_It__57ED0681404729F1");

            entity.ToTable("Order_Item");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_Ite__Order__4222D4EF");

            entity.HasOne(d => d.Prod).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProdId)
                .HasConstraintName("FK__Order_Ite__ProdI__412EB0B6");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("PK__Product___042785E5C54639B6");

            entity.ToTable("Product");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProdName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product_t__Categ__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User_tbl__1788CC4CC62DA94B");

            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
