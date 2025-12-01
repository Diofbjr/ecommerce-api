using Ecommerce.Api.Domain.Addresses;
using Ecommerce.Api.Domain.Orders;
using Ecommerce.Api.Domain.Tickets;
using Ecommerce.Api.Domain.Users;
using Ecommerce.Api.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        // Addresses
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PhoneVerification> PhoneVerifications { get; set; }

        // Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderReview> OrderReviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Store> Stores { get; set; }

        // Tickets
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        // Reviews globais
        public DbSet<RatingAspect> RatingAspects { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewAspectScore> ReviewAspectScores { get; set; }

        // Refresh Tokens
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        // ------------------------------------------------------
        // MODEL BUILDER CONFIG
        // ------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 Relacionamento User 1:N RefreshTokens
            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔥 Opcional: Email único
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
