using pozdravliator.Domain;
using pozdravliator.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection.Emit;

namespace pozdravliator.Infrastructure.DataAccess
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            /*modelBuilder.Entity<Photo>()
                .HasOne<Person>()
                .WithOne()
                .HasForeignKey<Person>(e => e.Id)
                .IsRequired();*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
