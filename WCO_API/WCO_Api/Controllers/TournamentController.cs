using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Data;
using WCO_Api.Models;
using WCO_Api.Repository;
using WCO_Api.WEBModels;

/*
 *Controlador donde se van a hacer peticiones para los torneos, se pueden hacer post y get 
*/

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

        /*
         * Obtiene una lista con objetos TournamentOut que se mandarán a la web, no recibe nada
         */

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

        /*
         * Obtiene una lista con objetos TournamentOut que se mandarán a la web, recibe el string con el id del torneo
         */

        [HttpGet("getTournamentById/{id}")]
        public async Task<List<TournamentOut>> getTournamentById(string id)
        {

            List<TournamentOut> allTournaments = new List<TournamentOut>();
            IEnumerable<Tournament> dbTournaments;

            dbTournaments = await managementRepository.getTournamentsById(id);

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

        /*
         * Obtiene una lista con objetos TeamWEB filtrándolos por medio de su tipo, el resultado se mandará al navegador, tiene de entrada un string type
         */

        [HttpGet("GetTeamsByType/{type}")]
        public async Task<IEnumerable<TeamWEB>> getTeamsByType(string type)
        {
            return await managementRepository.getTeamsByType(type);
        }


        /*
         * Obtiene una lista con objetos TeamWEB filtrándolos por medio del torneo al que pertenecen, el resultado se mandará al navegador, tiene de entrada un string con
         * el id del torneo
         */


        [HttpGet("GetTeamsByTournamentId/{id}")]
        public async Task<IEnumerable<TeamWEB>> GetTeamsByTournamentId(string id)
        {
            return await managementRepository.getTeamByTournamentId(id);
        }


        /*
         * Recibe un objeto TournamentWEB, se hacen las operaciones necesarias para que se meta la información a la base de datos.
         * Retorna un Task<IActionResult> que indica si la opracion tuvo exito o no
         */


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
