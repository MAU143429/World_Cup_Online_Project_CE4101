using Microsoft.AspNetCore.Mvc;
using System.Text;
using WCO_Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        // Data inicial para ir probando codigo

        private static List<Tournament> tournaments = new List<Tournament>();
        private static List<String> selectionTeams = new List<String> { "Costa Rica", "Alemania", "Portugal", "Colombia", "Argentina", "Brasil", "Uruguay", "Italia" };
        private static List<String> localTeams = new List<String> { "Bayern Munich", "Real Madrid", "Barcelona", "Tottenham" };

        // GET: api/<TorneoController>
        /* Función que obtiene una lista con todos los torneos registrados para un usuario
         * Entradas: Ninguna
         * Salida: Lista con torneos (JSON)
         * Restricciones: Ninguna
         */
        [HttpGet]
        public async Task<ActionResult<List<Tournament>>> Get()
        {
            return Ok(tournaments);
        }

        // GET api/<TorneoController>/5
        /* Función que me permite obtener la información de un torneo con su respectiva llave alfanumérica
         * Entradas: id de la llave alfanumérica como un string
         * Salidas: Información de un torneo en particular
         * Restricciones: ninguna, en caso de dar una llave incorrecta se devuelve un mensaje indicando esto
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> Get(string id)
        {
            var tournament = tournaments.Find(t => t.Id == id);
            if (tournament == null)
                return BadRequest("Tournament not found");

            List<Tournament> tour = new List<Tournament>();
            tour.Add(tournament);

            return Ok(tour);
        }

        // GET: api/getSelectionsTeams/<TorneoController>
        /* Función que obtiene una lista con todos los equipos de selecciones para un torneo
         * Entradas: Ninguna
         * Salida: Lista con selecciones (JSON)
         * Restricciones: Ninguna
         */
        [HttpGet("getSelectionsTeams")]
        public async Task<ActionResult<List<Tournament>>> getSelectionTeams()
        {
            return Ok(selectionTeams);
        }

        // GET api/<TorneoController>/5
        /* Función que me permite obtener la información de un torneo con su respectiva llave alfanumérica
         * Entradas: id de la llave alfanumérica como un string
         * Salidas: Información de un torneo en particular
         * Restricciones: ninguna, en caso de dar una llave incorrecta se devuelve un mensaje indicando esto
         */

        [HttpGet("getLocalTeams")]
        public async Task<ActionResult<List<Tournament>>> getLocalTeams()
        {
            return Ok(localTeams);
        }

        // POST api/<TorneoController>
        /* Función que permite agregar un torneo a la lista de torneos existentes
         * Entradas: Un JSON con la información del torneo de acuedo al modelo Tournament
         * Salidas: Devuelve la lista de torneos existentes una vez agrega el nuevo
         * Restricciones: Ninguna por el momento
         */

        [HttpPost]
        public async Task<ActionResult<List<Tournament>>> PostTournament(Tournament tournament)
        {
            //Se debería agregar a la base de datos el torneo en sí

            var newUuid = GetUUID();

            //Verifica si el uuid se repite

            while (!isUUIDUnique(newUuid))
            {
                newUuid = GetUUID();
            }
            tournament.Id = newUuid;

            tournaments.Add(tournament);
            return Ok(tournaments);
        }

        // PUT api/<TorneoController>/5
        [HttpPut("{id}")]
        public bool Put(string id)
        {
            return isUUIDUnique(id);
        }

        //----- Métodos para creacion y verificación de llaves alfanuméricas -------


        /* Función que me permite crear una llave alfanumérica única
         * Entradas: Ninguna
         * Salidas: Una llave alfanumérica
         * Restricciones: Ninguna
         */
        private string GetUUID()
        {

            var uuid = Guid.NewGuid();
            return uuid.ToString();

        }

        /* Función que me permite avergiguar si una llave alfanumérica creada es igual a otra
         * existente en la lista de torneos
         * Entradas: Una llave alfanumérica como string
         * Salidas: booleano indicando si está repetida o no
         * Restricciones: Entrada debe ser un string
         */

        private bool isUUIDUnique(string uuid)
        {

            //Se revisa si existe el UUID en la base de datos, en este caso se revisa tournaments
            //de manera local
            foreach (var dbTournament in tournaments)
            {
                if (dbTournament.Id == uuid)
                {
                    return false;
                }
            }

            return true;

        }

    }
}
