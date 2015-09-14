using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RobotGame.API.Controllers
{
	[RoutePrefix("api/robotGameProxy")]
	public class RobotGameController : ApiController
	{
		[Route("robots")]
		public IHttpActionResult GetRobotPlayers()
		{
			var client = new WebClient();
			var result = client.DownloadString(new Uri("https://robotgame.net/api/robot/stats"));
			var doc = (JArray)JsonConvert.DeserializeObject(result);
			var objs = doc
				.Where( d => d.Value<bool>("disabled") == false)
				.Select(d =>
					new RobotPlayer
					{
						id = d.Value<int>("id"),
						rating = d["rating"].Type == JTokenType.Null ? 0 : (int)d.Value<double>("rating"),
						name = d.Value<string>("name")
					});
			return Ok(objs);
		}
	}

	public class RobotPlayer
	{
		public int id { get; set; }
		public int rating { get; set; }
		public string name { get; set; }
	}
}
