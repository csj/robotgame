using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using RobotGame.API.Entities;
using RobotGame.API.Models;

namespace RobotGame.API
{
	public class RobotGameDbContext : IdentityDbContext<HumanPlayer>
    {
        public RobotGameDbContext()
            : base("RobotGameDbContext", throwIfV1Schema:false)
        {
     
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
		public DbSet<Game> Games { get; set; }

		public static RobotGameDbContext Create()
		{
			return new RobotGameDbContext();
		}
    }

}