using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mission10_Rosenlund.Data;

namespace Mission10_Rosenlund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlingLeagueController : ControllerBase
    {
        private IBowlingRepository _repo;
        public BowlingLeagueController(IBowlingRepository temp)
        {
            _repo = temp;
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {
            var bowlerData = from bowler in _repo.Bowlers
                             join team in _repo.Teams on bowler.TeamId equals team.TeamId into bowlerTeams
                             from team in bowlerTeams.DefaultIfEmpty() // Perform left outer join
                             select new
                             {
                                 bowler.BowlerId,
                                 bowler.BowlerLastName,
                                 bowler.BowlerFirstName,
                                 bowler.BowlerMiddleInit,
                                 bowler.BowlerAddress,
                                 bowler.BowlerCity,
                                 bowler.BowlerState,
                                 bowler.BowlerZip,
                                 bowler.BowlerPhoneNumber,
                                 bowler.TeamId,
                                 TeamName = team != null ? team.TeamName : null
                             };

            return bowlerData.ToArray();
        }

    }
}
