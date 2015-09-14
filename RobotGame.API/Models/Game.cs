using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace RobotGame.API.Models
{
	public class Game
	{
		private HumanPlayer _human1;
		private HumanPlayer _human2;

		[Key]
		public int GameId { get; set; }

		
		public HumanPlayer Human1
		{
			get { return _human1; }
			set
			{
				_human1 = value;
				Player1Name = value.UserName;
			}
		}

		public HumanPlayer Human2
		{
			get { return _human2; }
			set
			{
				_human2 = value;
				Player2Name = value.UserName;
			}
		}

		public int? RobotId { get; set; }
		public string Player1Name { get; set; }
		public string Player2Name { get; set; }
		public List<Position> Positions { get; set; }

		public Game()
		{
			Positions = new List<Position>();
		}

		public void SetUpInitialPosition()
		{
			var startPoints = new[]
			{
				new[] {3, 15},
				new[] {3, 14},
				new[] {2, 13},
				new[] {2, 12},
				new[] {1, 11},
				new[] {1, 10},
				new[] {1, 9},
				new[] {1, 8},
				new[] {1, 7},
				new[] {2, 6},
				new[] {2, 5},
				new[] {3, 4},
				new[] {3, 3},
				new[] {4, 3},
				new[] {5, 2},
				new[] {6, 2},
				new[] {7, 1},
				new[] {8, 1},
				new[] {9, 1},
				new[] {10, 1},
				new[] {11, 1},
				new[] {12, 2},
				new[] {13, 2},
				new[] {14, 3},
			}.ToList();

			var rand = new Random();
			Func<List<int[]>, int[]> remove = l =>
			{
				var i = rand.Next(l.Count);
				var result = l[i];
				l.RemoveAt(i);
				return result;
			};

			var position = new Position();
			Positions.Add(position);

			for (int i = 0; i < 5; i++)
			{
				var start = remove(startPoints);
				var p1 = rand.Next(2) + 1;  // 1 or 2
				position.Robots.Add(new Robot
				{
					Row = start[0],
					Col = start[1],
					Health = 50,
					Team = p1
				});

				position.Robots.Add(new Robot
				{
					Row = 18 - start[0],
					Col = 18 - start[1],
					Health = 50,
					Team = 3 - p1
				});
			}
		}
	}

	public class Position
	{
		[Key]
		public int PositionId { get; set; }

		[NotMapped]
		public List<Robot> Robots { get; set; }

		public Position() {  Robots = new List<Robot>(); }

		public string RobotsJSON
		{
			get { return JsonConvert.SerializeObject(Robots); }
			set { Robots = JsonConvert.DeserializeObject<List<Robot>>(value); }
		}
	}

	[NotMapped]
	public class Robot
	{
		public int Row { get; set; }
		public int Col { get; set; }
		public int Team { get; set; }
		public int Health { get; set; }
		
		[NotMapped]
		public MoveEnum? Move { get; set; }
		public string MoveString
		{
			get { return Move == null ? null : Move.ToString(); }
			set { Move = value == null ? (MoveEnum?)null : value.ToEnum<MoveEnum>(); }
		}
	}

	public enum MoveEnum
	{
		AU, AR, AD, AL, MU, MR, MD, ML, EX, BL
	}

	public class HumanPlayer : IdentityUser
	{
		public bool AcceptingChallenges { get; set; }
	}

	public static class EnumHelper
	{
		/// <summary>
		/// Converts string to enum value (opposite to Enum.ToString()).
		/// </summary>
		/// <typeparam name="T">Type of the enum to convert the string into.</typeparam>
		/// <param name="s">string to convert to enum value.</param>
		public static T ToEnum<T>(this string s) where T : struct
		{
			T newValue;
			return Enum.TryParse(s, out newValue) ? newValue : default(T);
		}
	}
}