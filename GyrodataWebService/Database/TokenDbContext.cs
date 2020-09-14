namespace GyrodataWebService.Database
{
    using System.Data.Entity;
    public partial class TokenDbContext : System.Data.Entity.DbContext
    {
        public TokenDbContext()
            : base("name=TokenDbContext")
        {
        }

        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Tokens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}