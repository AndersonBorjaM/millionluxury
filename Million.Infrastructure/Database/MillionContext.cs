using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Million.Application.Exceptions;
using Million.Domain.Abstractions;
using Million.Domain.Owners;
using Million.Domain.Properties;
using Million.Domain.PropertyImages;
using Million.Domain.PropertyTraces;
using Million.Domain.Users;

namespace Million.Repository.Database
{
    public class MillionContext : DbContext, IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly IPublisher _publisher;

        public DbSet<User> User { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyImage> PropertyImage { get; set; }
        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        public MillionContext(DbContextOptions<MillionContext> options, IConfiguration configuration, IPublisher publisher) : base(options)
        {
            _configuration = configuration;
            _publisher = publisher;
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

                builder.Property(p => p.Id).HasConversion(c => c!.Value, v => new(v)).ValueGeneratedOnAdd();
                builder.Property(p => p.UserName).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.PasswordHash).HasConversion(c => c!.Value, v => new(v));
            });

            modelBuilder.Entity<Owner>(builder =>
            {

                builder.ToTable("Owner");

                builder.HasKey(c => c.Id);


                builder.Property(p => p.Id).HasConversion(c => c!.Value, v => new(v)).HasColumnName("IdOwner").ValueGeneratedOnAdd();
                builder.Property(p => p.Name).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Birthday);
                builder.Property(p => p.Address).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Photo).HasConversion(c => c!.Value, v => new(v));
            });

            modelBuilder.Entity<Property>(builder =>
            {

                builder.ToTable("Property");

                builder.HasKey(c => c.Id);

                builder.Property(p => p.Id).HasConversion(c => c!.Value, v => new(v)).HasColumnName("IdProperty").ValueGeneratedOnAdd();
                builder.Property(p => p.Name).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Address).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Price).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Year).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.CodeInternal).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.IdOwner).HasConversion(c => c!.Value, v => new(v));
            });

            modelBuilder.Entity<PropertyImage>(builder =>
            {

                builder.ToTable("PropertyImage");

                builder.HasKey(c => c.Id);

                builder.Property(p => p.Id).HasConversion(c => c!.Value, v => new(v)).HasColumnName("IdPropertyImage").ValueGeneratedOnAdd();
                builder.Property(p => p.IdProperty).HasConversion(c => c.Value, v => new(v)).HasColumnName("IdProperty");
                builder.Property(p => p.Enabled).HasConversion(c => c.Value, v => new(v));
                builder.Property(p => p.File).HasConversion(c => c.Value, v => new(v)).HasColumnName("FileProperty");
            });

            modelBuilder.Entity<PropertyTrace>(builder =>
            {

                builder.ToTable("PropertyTrace");

                builder.HasKey(c => c.Id);

                builder.Property(p => p.Id).HasConversion(c => c!.Value, v => new(v)).HasColumnName("IdPropertyTrace").ValueGeneratedOnAdd();
                builder.Property(p => p.DateSale);
                builder.Property(p => p.Name).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Tax).HasConversion(c => c!.Value, v => new(v));
                builder.Property(p => p.Value).HasConversion(c => c!.ValueProperyTrace, v => new(v));
                builder.Property(p => p.IdProperty).HasConversion(c => c!.Value, v => new(v));

            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventsAsync();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }

        }

    }
}
