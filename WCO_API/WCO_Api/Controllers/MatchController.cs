using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Data;
using WCO_Api.WEBModels;

/**
 * Controlador encargado de realizar las acciones relacionadas con los torneos, acciones como get, post, put
 * 
*/
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

        /**
         * 
         */

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

        /**
         * Obtiene los partidos por bracket id
         * Revibe un entero que representa el id del bracket en la base de datos
         */

        [HttpGet("getMatchesByBracketId/{id}")]
        public async Task<List<List<MatchOut>>> getMatchesByTournamentId(string id)
        {

            List<BracketWEB> allBrackets = (List<BracketWEB>)await managementRepository.getBracketsByTournamentId(id);
            List<List<MatchOut>> allMatches = new List<List<MatchOut>>();

            foreach (var bracket in allBrackets)
            {
                List<MatchOut> matchesWeb = new List<MatchOut>();

                matchesWeb = (List<MatchOut>)await managementRepository.getMatchesByBracketId(bracket.BId);

                foreach (MatchOut dbMatch in matchesWeb)
                {

                    List<TeamWEB> teams = (List<TeamWEB>)await managementRepository.getTeamsByMatchId(dbMatch.MId);
                    dbMatch.teams = teams;

                }

                allMatches.Add(matchesWeb);

            }
            
            return allMatches;
        }
    }
}
