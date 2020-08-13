
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Monster.Inc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monster.Inc.Web.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public virtual DbSet<Intimidiator> Intimidiators { get; set; }
        public virtual DbSet<Door> Doors { get; set; }

        public virtual DbSet<WorkDay> WorkDays { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Intimidiator>()
                    .HasIndex(i => i.ApplicationUserId)
                    .IsUnique();

            modelBuilder.Entity<Door>().HasData(
                new Door { Energy = 90, Name = "Yellow", Id = 1 },
                new Door { Energy = 100, Name = "Red", Id = 2 },
                new Door { Energy = 110, Name = "Pink", Id = 3 },
                new Door { Energy = 220, Name = "Black", Id = 4 },
                new Door { Energy = 240, Name = "Green", Id = 5 },
                new Door { Energy = 140, Name = "Blue", Id = 6 },
                new Door { Energy = 160, Name = "Putple", Id = 7 },
                new Door { Energy = 160, Name = "Grey", Id = 8 }
            );
        }
    }
}
