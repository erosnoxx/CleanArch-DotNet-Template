using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class RestauranteEntityConfigurations : IEntityTypeConfiguration<RestauranteEntity>
    {
        public void Configure(EntityTypeBuilder<RestauranteEntity> builder)
        {
            builder.ToTable("Restaurantes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Cnpj)
                .IsRequired()
                .HasMaxLength(14);
        }
    }
}
