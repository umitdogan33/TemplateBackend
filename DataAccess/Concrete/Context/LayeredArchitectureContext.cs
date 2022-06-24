using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccess.Concrete.Context
{
   public class LayeredArchitectureContext:DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // sql server address can be changed
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LayeredArchitecture;Trusted_Connection=True");
            //(LocalDB)\\MSSQLLocalDB
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
