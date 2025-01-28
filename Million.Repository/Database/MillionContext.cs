using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Million.Domain.Models;

namespace Million.Repository.Database
{
    public class MillionContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> User { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        public MillionContext(DbContextOptions<MillionContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User");

                builder.HasKey(c => c.Id);

                builder.Property(c => c.Id).IsRequired();
                builder.Property(c => c.UserName).IsRequired();
                builder.Property(c => c.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Owner>(builder =>
            {

                builder.ToTable("Owner");

                builder.HasKey(c => c.IdOwner);

                builder.Property(c => c.IdOwner).IsRequired();
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Birthday).IsRequired();
                builder.Property(c => c.Address).IsRequired();
                builder.Property(c => c.Photo);

                builder.HasMany(c => c.Properties).WithOne(c => c.Owner).HasForeignKey(c => c.IdOwner).HasPrincipalKey(c => c.IdOwner);
            });

            modelBuilder.Entity<Property>(builder =>
            {

                builder.ToTable("Property");

                builder.HasKey(c => c.IdProperty);

                builder.Property(c => c.IdProperty).IsRequired();
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Address).IsRequired();
                builder.Property(c => c.Price).IsRequired();
                builder.Property(c => c.Year).IsRequired();
                builder.Property(c => c.CodeInternal).IsRequired();
                builder.Property(c => c.IdOwner).IsRequired();


                builder.HasMany(c => c.PropertyTraces).WithOne(c => c.Property).HasForeignKey(c => c.IdProperty).HasPrincipalKey(c => c.IdProperty);
                builder.HasMany(c => c.PropertyImages).WithOne(c => c.Property).HasForeignKey(c => c.IdProperty).HasPrincipalKey(c => c.IdProperty);
            });

            modelBuilder.Entity<PropertyImage>(builder =>
            {

                builder.ToTable("PropertyImage");

                builder.HasKey(c => c.IdPropertyImage);

                builder.Property(c => c.IdPropertyImage).IsRequired();
                builder.Property(c => c.IdProperty).IsRequired();
                builder.Property(c => c.Enabled);
                builder.Property(c => c.FileProperty);

            });

            modelBuilder.Entity<PropertyTrace>(builder =>
            {

                builder.ToTable("PropertyTrace");

                builder.HasKey(c => c.IdPropertyTrace);

                builder.Property(c => c.IdPropertyTrace).IsRequired();
                builder.Property(c => c.DateSale);
                builder.Property(c => c.Name).IsRequired();
                builder.Property(c => c.Tax).IsRequired();
                builder.Property(c => c.Value).IsRequired();

            });
        }
    }
}
