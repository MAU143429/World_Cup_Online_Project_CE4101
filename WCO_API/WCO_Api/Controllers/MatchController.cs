using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Data;
using WCO_Api.WEBModels;

namespace WCO_Api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        ManagementRepository managementRepository = new ManagementRepository();

        public MatchController()
        {

        }

        [HttpPost("AddMatch")]
        public async Task<IActionResult> createMatch([FromBody] MatchWEB match)
        {

            if (match == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var total = await managementRepository.getTotalMatches();

            match.MId = total + 1;
            match.startTime = match.startTime;
            match.date = match.date;
            match.venue = match.venue;
            match.bracketId = match.bracketId;

            //guardar relacion partidos

            var created = await managementRepository.createNewMatch(match);

            return Created("created", created);

        }

        [HttpPost("getMatchesByBracketId")]
        public async Task<List<MatchOut>> getMatchesByBracketId(int id)
        {

            List<MatchOut> matchesWeb= new List<MatchOut>();

            matchesWeb = (List<MatchOut>)await managementRepository.getMatchesByBracketId(id);

            foreach (MatchOut dbMatch in matchesWeb) {

                Console.WriteLine("PARTIDOS DEL BRACKET" + id);
                Console.WriteLine(dbMatch.MId);

                List<TeamWEB> teams = (List<TeamWEB>)await managementRepository.getTeamsByMatchId(dbMatch.MId);
                dbMatch.teams = teams;
   
            }
            
            return matchesWeb;
        }
    }
}
