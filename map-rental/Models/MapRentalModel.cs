namespace map_rental.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MapRentalModel : DbContext
    {
        public MapRentalModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Rental>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Rental>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Rental>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Rental>()
                .Property(e => e.Rent)
                .HasPrecision(10, 1);

            modelBuilder.Entity<Rental>()
                .Property(e => e.Contact)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Rentals)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<User>()
            //    .HasOptional(e => e.User1)
            //    .WithRequired(e => e.User2);
        }
    }
}
