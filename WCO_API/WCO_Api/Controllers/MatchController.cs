using Microsoft.AspNetCore.Mvc;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

/**
 * Controlador encargado de realizar las acciones relacionadas con los Partidos, acciones como get, post, put
 * 
*/
namespace WCO_Api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        MatchRepository matchRepository = new MatchRepository();
        TournamentRepository tournamentRepository = new TournamentRepository();

        /**
         * Petición que se encarga de agregar un partido a la tabla Matches
         */

        [HttpPost("AddMatch")]
        public async Task<IActionResult> createMatch([FromBody] MatchWEB match)
        {

            if (match == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            match.startTime = match.startTime;
            match.date = match.date;
            match.venue = match.venue;
            match.bracketId = match.bracketId;

            var created = await matchRepository.createNewMatch(match);

            return Created("created", created);

        }

        /**
         * Obtiene los partidos por bracket id
         * Revibe un entero que representa el id del bracket en la base de datos
         */

        [HttpGet("getMatchesByTournamentId/{id}")]
        public async Task<List<List<MatchOut>>> getMatchesByTournamentId(string id)
        {

            List<BracketWEB> allBrackets = (List<BracketWEB>)await tournamentRepository.getBracketsByTournamentId(id);
            List<List<MatchOut>> allMatches = new List<List<MatchOut>>();

            foreach (var bracket in allBrackets)
            {
                List<MatchOut> matchesWeb = new List<MatchOut>();

                matchesWeb = (List<MatchOut>)await matchRepository.getMatchesByBracketId(bracket.BId);

                foreach (MatchOut dbMatch in matchesWeb)
                {

                    List<TeamWEB> teams = (List<TeamWEB>)await matchRepository.getTeamsByMatchId(dbMatch.MId);
                    dbMatch.teams = teams;

                }

                allMatches.Add(matchesWeb);

            }
            
            return allMatches;
        }

        [HttpGet("GetMatchbyId/{id}")]
        public async Task<List<MatchOut>> getMatchById(int id)
        {

            return (List<MatchOut>)await matchRepository.getMatchById(id);

        }

        [HttpGet("GetPlayersbyTeamId/{id}")]
        public async Task<List<PlayerWEB>> GetPlayersbyTeamId(int id)
        {

            return (List<PlayerWEB>)await matchRepository.getPlayersbyTeamId(id);

        }

        [HttpGet("GetPlayersbyBothTeamId/{id1}/{id2}")]
        public async Task<List<PlayerWEB>> GetPlayersbyBothTeamId(int id1, int id2)
        {

            return (List<PlayerWEB>)await matchRepository.getPlayersbyBothTeamId(id1, id2);

        }

    }
}
