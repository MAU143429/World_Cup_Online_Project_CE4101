using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Data;
using WCO_Api.Models;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        ManagementRepository managementRepository = new ManagementRepository();

        public TournamentController()
        {

        }

        [HttpGet]
        public async Task<List<TournamentOut>> getTournaments()
        {

            List<TournamentOut> allTournaments = new List<TournamentOut>();
            IEnumerable<Tournament> dbTournaments;

            dbTournaments = await managementRepository.getTournaments();


            foreach (var tournament in dbTournaments)
            {
                TournamentOut newTournament = new TournamentOut();

                newTournament.ToId = tournament.ToId;
                newTournament.Name = tournament.Name;
                newTournament.StartDate = tournament.StartDate;
                newTournament.EndDate = tournament.EndDate;
                newTournament.Description = tournament.Description;
                newTournament.Type = tournament.Type;
                newTournament.teams = (List<TeamWEB>)await managementRepository.getTeamByTournamentId(tournament.ToId);

                List<BracketWEB> brackets = new List<BracketWEB>();

                brackets = (List<BracketWEB>)await managementRepository.getBracketsByTournamentId(tournament.ToId);

                newTournament.brackets = brackets;

                allTournaments.Add(newTournament);

            }

            return allTournaments;
        }

        [HttpGet("getTournamentById")]
        public async Task<List<TournamentOut>> getTournamentById()
        {

            List<TournamentOut> allTournaments = new List<TournamentOut>();
            IEnumerable<Tournament> dbTournaments;

            dbTournaments = await managementRepository.getTournaments();


            foreach (var tournament in dbTournaments)
            {
                TournamentOut newTournament = new TournamentOut();

                newTournament.ToId = tournament.ToId;
                newTournament.Name = tournament.Name;
                newTournament.StartDate = tournament.StartDate;
                newTournament.EndDate = tournament.EndDate;
                newTournament.Description = tournament.Description;
                newTournament.Type = tournament.Type;
                newTournament.teams = (List<TeamWEB>)await managementRepository.getTeamByTournamentId(tournament.ToId);

                List<BracketWEB> brackets = new List<BracketWEB>();

                brackets = (List<BracketWEB>)await managementRepository.getBracketsByTournamentId(tournament.ToId);

                newTournament.brackets = brackets;

                allTournaments.Add(newTournament);

            }

            return allTournaments;
        }

        [HttpGet("GetTeamsByType/{type}")]
        public async Task<IEnumerable<TeamWEB>> getTeamsByType(string type)
        {
            return await managementRepository.getTeamsByType(type);
        }

        [HttpGet("GetTeamsByTournamentId/{id}")]
        public async Task<IEnumerable<TeamWEB>> GetTeamsByTournamentId(string id)
        {
            return await managementRepository.getTeamByTournamentId(id);
        }

        [HttpPost("AddTournament")]
        public async Task<IActionResult> createTournament([FromBody] TournamentWEB tournament)
        {
            if (tournament == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Crear una llave y asignarla
            MyIdGenerator idGenerator = new MyIdGenerator();
            var newuuid = idGenerator.GetUUID();
            
            while (!idGenerator.isUUIDUnique(newuuid).Result)
            {
                
                newuuid = idGenerator.GetUUID();
            }

            tournament.ToId = newuuid;

            
            Tournament to = new Tournament();
            to.ToId = newuuid;
            to.Name = tournament.Name;
            to.StartDate = tournament.StartDate;
            to.EndDate = tournament.EndDate;
            to.Description = tournament.Description;
            to.Type = tournament.Type;

            var created = await managementRepository.createNewTournament(to);
            
            //Agregar Brackets webmodel

            foreach (var bracket in tournament.brackets)
            {
                Bracket newBracket = new Bracket();

                var total = await managementRepository.getTotalBrackets();

                newBracket.BId = total + 1;
                newBracket.Name = bracket;
                newBracket.TournamentId = newuuid;

                var create2 = await managementRepository.createNewBracket(newBracket);

            }
            //Put a equipo

            foreach (var team in tournament.teams)
            {
                Team dbTeam= new Team();

                dbTeam.TeId = team;
                dbTeam.TournamentId = newuuid;

                var created3 = await managementRepository.updateTeamId(dbTeam);

            }

            return Created("created", created);
        }
    }
}
