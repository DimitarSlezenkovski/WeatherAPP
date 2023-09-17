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
                .ToTable(typeof(T).Name + "s", "weatherdb");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.CreatedOn)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .IsRequired(true);

            builder
                .Property(x => x.DeletedOn)
                .HasColumnType("datetime(6)")
                .HasDefaultValueSql("NULL")
                .IsRequired(false);

            return builder;
        }
    }
}
