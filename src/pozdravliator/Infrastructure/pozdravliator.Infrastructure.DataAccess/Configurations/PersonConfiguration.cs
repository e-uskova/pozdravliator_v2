using pozdravliator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pozdravliator.Infrastructure.DataAccess.Configurations
{
    /// <summary>
    /// Конфигурация таблицы Persons.
    /// </summary>
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(e => e.Photo).WithOne(e => e.Person).HasForeignKey<Photo>("PersonId").IsRequired();
        }
    }
}
