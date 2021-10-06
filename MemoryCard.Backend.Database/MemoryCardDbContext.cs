using MemoryCard.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryCard.Backend.Database
{
    public class MemoryCardDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCredentialsModel>()
                .HasData(new[]{
                    new UserCredentialsModel{ Username = "nick", Password = "1234" }
                });
        }

    }
}

