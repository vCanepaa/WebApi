using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.DataBase
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
                
        }
        private void PersonTypeBuilder(EntityTypeBuilder<Person> e)
        {
            e.ToTable("person", "dbo");
            e.HasKey(p => p.Id);

            e.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property(p => p.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsRequired();

            e.Property(p => p.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsRequired();

            e.Property(p => p.Address)
                .HasColumnName("address")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            e.Property(p => p.Gender)
                .HasColumnName("gender")
                .HasColumnType("varchar")
                .HasMaxLength(6)
                .IsRequired();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(PersonTypeBuilder);
            modelBuilder.Entity<Book>(BookTypeBuilder);
        }

        private void BookTypeBuilder(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books", "dbo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title).HasColumnName("title")
                .HasColumnType("varchar(MAX)").IsRequired();

            builder.Property(p => p.Author).HasColumnName("author")
              .HasColumnType("varchar(MAX)").IsRequired();

            builder.Property(p => p.Price).HasColumnName("price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.LaunchDate).HasColumnName("launch_date")
                .IsRequired();
        }

        public  DbSet<Person> Persons { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
