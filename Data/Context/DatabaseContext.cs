using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Usuarios { get; set; }
        public DbSet<RestauranteEntity> Restaurantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Schema");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedAt = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }

                var dateProperties = entry.Entity.GetType()
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

                foreach (var property in dateProperties)
                {
                    var currentValue = (DateTime?)property.GetValue(entry.Entity);

                    if (currentValue.HasValue && currentValue.Value.Kind == DateTimeKind.Local)
                    {
                        property.SetValue(entry.Entity, currentValue.Value.ToUniversalTime());
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
