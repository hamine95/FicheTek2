using BackEnd.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Database
{
    public class DBContext : DbContext
    {
        public DbSet<FicheTechnique> FicheTec { get; set; }

        public DbSet<Produit> Product { get; set; }

        public DbSet<Machine> mMachine { get; set; }

        public DbSet<chaine> Chaine { get; set; }

        public DbSet<Composant> Composant { get; set; }

        public DbSet<Composition> Composition { get; set; }

        public DbSet<Couleur> Color { get; set; }

        public DbSet<Duitages> Duitages { get; set; }

        public DbSet<Enfilage> Enfilage { get; set; }

        public DbSet<EnfilageMatrix> EnfilageMatrix { get; set; }

        public DbSet<Matiere> Matiere { get; set; }

        public DbSet<ModelMachine> ModelMachine { get; set; }

        public DbSet<Produit> Produit { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var ConString = new SqliteConnectionStringBuilder
            {
                DataSource = "FicheTeK",
                Password = "TTBM1971"
            };
            var sqlite = new SqliteConnection(ConString.ToString());
            optionsBuilder.UseSqlite(sqlite);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chaine>().ToTable(nameof(Chaine));

            modelBuilder.Entity<Composant>().ToTable(nameof(Composant));
            modelBuilder.Entity<Couleur>().ToTable(nameof(Color));
            modelBuilder.Entity<Duitages>().ToTable(nameof(Duitages));

            modelBuilder.Entity<Enfilage>().ToTable(nameof(Enfilage));
            modelBuilder.Entity<EnfilageMatrix>().ToTable(nameof(EnfilageMatrix));
            modelBuilder.Entity<FicheTechnique>().ToTable(nameof(FicheTec));
            modelBuilder.Entity<Machine>().ToTable(nameof(mMachine));
            modelBuilder.Entity<Matiere>().ToTable(nameof(Matiere));
            modelBuilder.Entity<ModelMachine>().ToTable(nameof(ModelMachine));
            modelBuilder.Entity<Produit>().ToTable(nameof(Product));
        }
    }
}