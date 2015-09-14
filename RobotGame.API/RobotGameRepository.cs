using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RobotGame.API.Entities;
using RobotGame.API.Models;

namespace RobotGame.API
{

    public class RobotGameRepository : IDisposable
    {
		private RobotGameDbContext _ctx;

        private UserManager<HumanPlayer> _userManager;

        public RobotGameRepository(RobotGameDbContext ctx = null)
        {
            _ctx = ctx ?? new RobotGameDbContext();
			_userManager = new UserManager<HumanPlayer>(new UserStore<HumanPlayer>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserViewModel userViewModel)
        {
			HumanPlayer user = new HumanPlayer
            {
                UserName = userViewModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userViewModel.Password);

            return result;
        }

		public async Task<HumanPlayer> FindUser(string userName, string password)
        {
			HumanPlayer user = await _userManager.FindAsync(userName, password);
            return user;
        }

		public HumanPlayer FindUser(string userName)
	    {
		    return _ctx.Users.SingleOrDefault(u => u.UserName == userName);
	    }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

           var existingToken = _ctx.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

           if (existingToken != null)
           {
             var result = await RemoveRefreshToken(existingToken);
           }
          
            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
           var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

           if (refreshToken != null) {
               _ctx.RefreshTokens.Remove(refreshToken);
               return await _ctx.SaveChangesAsync() > 0;
           }

           return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
             return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
             return  _ctx.RefreshTokens.ToList();
        }

		public async Task<HumanPlayer> FindAsync(UserLoginInfo loginInfo)
        {
			HumanPlayer user = await _userManager.FindAsync(loginInfo);

            return user;
        }

		public async Task<IdentityResult> CreateAsync(HumanPlayer user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}