using Microsoft.EntityFrameworkCore;


namespace BusinessManager.Model
{
    public class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Сounterparty> Сounterparty { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<LegalEntity> LegalEntity { get; set; }
        public DbSet<Entrance> Entrance { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Article> Article { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Projects>(b =>
            //{
            //    b.HasKey(e => e.ProjectsId);
            //    b.Property(e => e.ProjectsId).ValueGeneratedOnAdd();
            //});
          
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = MIKEBOOK\SQLEXPRESS; Database = BuisnessManeger; Trusted_Connection = True; ");
        
        }
    
    }
}
