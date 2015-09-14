using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RobotGame.API.Models;

namespace RobotGame.API.Controllers
{
	[RoutePrefix("api/games")]
	public class GamesController : ApiController
	{
		private readonly RobotGameDbContext _ctx;
		private readonly RobotGameRepository _repo;

		public GamesController()
		{
			_ctx = new RobotGameDbContext();
			_repo = new RobotGameRepository(_ctx);
		}

		[Authorize]
		[Route("")]
		public IHttpActionResult Get()
		{
			var me = _repo.FindUser(User.Identity.GetUserName()).Id;

			var gameList = _ctx.Games
					.Where(g => g.Human1.Id == me)
					.Select(g => new {id=g.GameId, enemy = g.Player2Name, player = 1, positions = g.Positions}).ToList()
				.Union(_ctx.Games
					.Where(g => g.Human2.Id == me)
					.Select(g => new {id=g.GameId, enemy = g.Player1Name, player = 2, positions = g.Positions }).ToList()
				).OrderBy(q =>q.id).ToList();

			var gameDtos = from game in gameList
				select new GameDTO
				{
					enemy = game.enemy,
					needsMove = true,
					states = game.positions.Select(p => new GameStateDTO
					{
						robots = p.Robots.Select(r => new RobotDTO
						{
							action = r.MoveString,
							col = r.Col,
							row = r.Row,
							good = r.Team == game.player,
							health = r.Health
						}).ToList()
					}).ToList()
				};

			return Ok(gameDtos);
		}

		[Authorize]
		[Route("getHumanOpponents")]
		public IHttpActionResult GetReadyHumans()
		{
			var me = _repo.FindUser(User.Identity.GetUserName()).Id;
			var humans = _ctx.Users.Where(u => u.AcceptingChallenges).Select(u => u.UserName).ToList();

			return Ok(humans);
		}


		[Authorize]
		[Route("setReady")]
		[HttpPost]
		public IHttpActionResult SetReadyForChallenges(AcceptingChallengesDTO body)
		{
			var me = _repo.FindUser(User.Identity.GetUserName());
			me.AcceptingChallenges = body.ready;
			_ctx.SaveChanges();

			return Ok();
		}

		[Authorize]
		[Route("startHumanGame")]
		[HttpPost]
		public async Task<IHttpActionResult> StartHumanGame(StartHumanGameDTO body)
		{
			var enemyName = body.name;
			var enemy = _repo.FindUser(enemyName);
			if (enemy == null)
			{
				return NotFound();
			}
			var me = _repo.FindUser(User.Identity.GetUserName());

			var game = new Game { Human1 = me, Human2 = enemy };
			game.SetUpInitialPosition();
			_ctx.Games.Add(game);
			await _ctx.SaveChangesAsync();

			return Ok(); // todo, return id of new game to auto-highlight it in subsequent /games screen
		}

		public class AcceptingChallengesDTO
		{
			public bool ready { get; set; } // GO!
		}

		public class StartHumanGameDTO
		{
			public string name { get; set; }
		}

	}

	class RobotDTO
	{
		public bool good { get; set; }
		public int health { get; set; }
		public int row { get; set; }
		public int col { get; set; }
		public string action { get; set; }
	}

	class GameStateDTO
	{
		public List<RobotDTO> robots { get; set; }
		public GameStateDTO() { robots = new List<RobotDTO>(); }
	}

	class GameDTO
	{
		public string enemy { get; set; }
		public bool needsMove { get; set; }
		public List<GameStateDTO> states { get; set; }

		public GameDTO() { states = new List<GameStateDTO>(); }

		public static List<GameDTO> CreateDummyData()
		{
			return new List<GameDTO>
			{
				new GameDTO
				{
					enemy = "Peetee",
					needsMove = true,
					states =
					{
						new GameStateDTO(),
						new GameStateDTO
						{
							robots =
							{
								  new RobotDTO{                              
                                    good =  true,
                                    health =  50,
                                    row =  10,
                                    col =  10,
                                    action =  "AR"
                                },
								  new RobotDTO{                              
                                    good =  true,
                                    health =  50,
                                    row =  11,
                                    col =  11,
                                    action =  "AU"
                                },
                                new RobotDTO{
                                    good =  false,
                                    health =  50,
                                    row =  10,
                                    col =  11,
                                    action =  "AL"
                                },
							}
						},
						new GameStateDTO
						{
							robots =
							{
								  new RobotDTO{                              
                                    good =  true,
                                    health =  41,
                                    row =  10,
                                    col =  10,
                                },
								  new RobotDTO{                              
                                    good =  true,
                                    health =  50,
                                    row =  11,
                                    col =  11,
                                },
                                new RobotDTO{
                                    good =  false,
                                    health =  32,
                                    row =  10,
                                    col =  11,
                                },
							}
						}

					}
				}
			};
		}
	}
}
