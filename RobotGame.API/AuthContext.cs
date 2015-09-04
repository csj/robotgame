using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using RobotGame.API.Entities;

namespace RobotGame.API
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext", throwIfV1Schema:false)
        {
     
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }

}