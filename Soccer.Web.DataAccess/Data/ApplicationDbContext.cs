using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Models.Entities;
using System.Linq;

namespace Soccer.Web.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            /*esta linea sirve para evitar el borrado en cascada, la idea por ejemplo es que si uno borra un usuario, pues no borre los productos*/
            var cascadeFks = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFks)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<GroupDetailEntity> GroupDetails { get; set; }

        public DbSet<GroupEntity> Groups { get; set; }

        public DbSet<MatchEntity> Matches { get; set; }

        public DbSet<PredictionEntity> Predictions { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<TournamentEntity> Tournaments { get; set; }

    }
}
