using Microsoft.AspNetCore.Mvc;
using WCO_Api.Logic;
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

        TournamentRepository tournamentRepository = new TournamentRepository();

        /*
         * Obtiene una lista con objetos TournamentOut que se mandarán a la web, no recibe nada
         */

        [HttpGet]
        public async Task<List<TournamentOut>> getTournaments()
        {

            List<TournamentOut> allTournaments = new List<TournamentOut>();
            IEnumerable<TournamentOut> dbTournaments;

            dbTournaments = await tournamentRepository.getTournaments();


            foreach (var tournament in dbTournaments)
            {
                TournamentOut newTournament = new TournamentOut();

                newTournament.ToId = tournament.ToId;
                newTournament.Name = tournament.Name;
                newTournament.StartDate = tournament.StartDate;
                newTournament.EndDate = tournament.EndDate;
                newTournament.Description = tournament.Description;
                newTournament.Type = tournament.Type;
                newTournament.teams = (List<TeamWEB>)await tournamentRepository.getTeamByTournamentId(tournament.ToId);

                List<BracketWEB> brackets = new List<BracketWEB>();

                brackets = (List<BracketWEB>)await tournamentRepository.getBracketsByTournamentId(tournament.ToId);

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
            IEnumerable<TournamentOut> dbTournaments;

            dbTournaments = await tournamentRepository.getTournamentsById(id);

            foreach (var tournament in dbTournaments)
            {
                TournamentOut newTournament = new TournamentOut();

                newTournament.ToId = tournament.ToId;
                newTournament.Name = tournament.Name;
                newTournament.StartDate = tournament.StartDate;
                newTournament.EndDate = tournament.EndDate;
                newTournament.Description = tournament.Description;
                newTournament.Type = tournament.Type;
                newTournament.teams = (List<TeamWEB>)await tournamentRepository.getTeamByTournamentId(tournament.ToId);

                List<BracketWEB> brackets = new List<BracketWEB>();

                brackets = (List<BracketWEB>)await tournamentRepository.getBracketsByTournamentId(tournament.ToId);

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
            return await tournamentRepository.getTeamsByType(type);
        }


        /*
         * Obtiene una lista con objetos TeamWEB filtrándolos por medio del torneo al que pertenecen, el resultado se mandará al navegador, tiene de entrada un string con
         * el id del torneo
         */


        [HttpGet("GetTeamsByTournamentId/{id}")]
        public async Task<IEnumerable<TeamWEB>> GetTeamsByTournamentId(string id)
        {
            return await tournamentRepository.getTeamByTournamentId(id);
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
            MyIdGenerator generator = new MyIdGenerator();

            string newuuid = generator.GetUUID();

            tournament.ToId = newuuid;

            var created = await tournamentRepository.createNewTournament(tournament);
            
            return Created("created", created);
        }
    }
}
