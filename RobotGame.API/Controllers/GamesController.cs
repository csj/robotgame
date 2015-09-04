using System.Collections.Generic;
using System.Web.Http;

namespace RobotGame.API.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;

            return Ok(Game.CreateDummyData());
        }
    }

	class Robot
	{
		public bool good { get; set; }
		public int health { get; set; }
		public int row { get; set; }
		public int col { get; set; }
		public string action { get; set; }
	}

	class GameState
	{
		public List<Robot> robots { get; set; }
		public GameState() {  robots = new List<Robot>(); }
	}

	class Game
	{
		public string enemy { get; set; }
		public int myScore { get; set; }
		public int enemyScore { get; set; }
		public bool needsMove { get; set; }
		public List<GameState> states { get; set; }

		public Game() {  states = new List<GameState>();}

		public static List<Game> CreateDummyData()
		{
			return new List<Game>
			{
				new Game
				{
					enemy = "Peetee",
					myScore = 10,
					enemyScore = 4,
					needsMove = true,
					states =
					{
						new GameState(),
						new GameState
						{
							robots =
							{
								  new Robot{                              
                                    good =  true,
                                    health =  50,
                                    row =  1,
                                    col =  11,
                                    action =  "ML"
                                },
                                new Robot{
                                    good =  true,
                                    health =  50,
                                    row =  2,
                                    col =  12,
                                    action =  "ML"
                                },
                               new Robot {
                                    good =  true,
                                    health =  50,
                                    row =  3,
                                    col =  12,
                                    action =  "AR"
                                },
                                new Robot{
                                    good =  true,
                                    health =  50,
                                    row =  4,
                                    col =  11,
                                    action =  "MD"
                                },
                               new Robot {
                                    good =  true,
                                    health =  50,
                                    row =  4,
                                    col =  13,
                                    action =  "EX"
                                },
                                new Robot{
                                    good =  false,
                                    health =  50,
                                    row =  3,
                                    col =  13,
                                    action =  "AL"
                                },
                               new Robot {
                                    good =  false,
                                    health =  50,
                                    row =  3,
                                    col =  14,
                                    action =  "MD"
                                },
                                new Robot{
                                    good =  false,
                                    health =  50,
                                    row =  5,
                                    col =  13,
                                    action =  "EX"
                                },
                                new Robot{
                                    good =  false,
                                    health =  50,
                                    row =  5,
                                    col =  15,
                                    action =  "BL"
                                },
                                new Robot{
                                    good =  false,
                                    health =  50,
                                    row =  6,
                                    col =  14,
                                    action =  "AL"
                                }

							}
						}
					}
				}
			};
		}
	}

	/*
	 * return [
                {
                    enemy: "Peetee",
                    myScore: 5,
                    enemyScore: 5,
                    needsMove: true,
                    states: [
                        {},
                        {
                            robots: [
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 11,
                                    action: 'ML'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 2,
                                    col: 12,
                                    action: 'ML'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 3,
                                    col: 12,
                                    action: 'AR'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 4,
                                    col: 11,
                                    action: 'MD'
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 4,
                                    col: 13,
                                    action: 'EX'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 3,
                                    col: 13,
                                    action: 'AL'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 3,
                                    col: 14,
                                    action: 'MD'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 13,
                                    action: 'EX'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 15,
                                    action: 'BL'
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14,
                                    action: 'AL'
                                }
                            ]
                        },
                        {
                            robots: [
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 10
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 2,
                                    col: 11
                                },
                                {
                                    good: true,
                                    health: 42,
                                    row: 3,
                                    col: 12
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 5,
                                    col: 11
                                },
                                {
                                    good: false,
                                    health: 41,
                                    row: 3,
                                    col: 13
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 4,
                                    col: 14
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 5,
                                    col: 15
                                },
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14
                                }
                            ]
                        }
                    ]
                },
                {
                    name: "Game 2",
                    needsMove: false,
                    enemy: "WhiteHalmos",
                    states: [
                        { robots: [] }, {
                            robots: [
                                {
                                    good: false,
                                    health: 50,
                                    row: 6,
                                    col: 14
                                },
                                {
                                    good: true,
                                    health: 50,
                                    row: 1,
                                    col: 10,
                                    action: 'AD'
                                }
                            ]
                        }
                    ]
                }
            ];
	 */
}
