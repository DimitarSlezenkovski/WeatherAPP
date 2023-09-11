using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherAPP.Data.Entities;

namespace WeatherAPP.Data.Maps
{
    public static class MapExtensions
    {
        public static EntityTypeBuilder<T> MapEntity<T>(this EntityTypeBuilder<T> builder) where T : class, IEntity
        {
            builder
                .ToTable(typeof(T).Name, "db");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.CreatedOn)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired(true);

            builder
                .Property(x => x.DeletedOn)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("NULL")
                .IsRequired(false);

            return builder;
        }
    }
}
