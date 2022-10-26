using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Data;
using WCO_Api.Models;
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
        public async Task<IEnumerable<Tournament>> getTournaments()
        {
            return await managementRepository.getTournaments();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> createTournament([FromBody] TournamentWEB tournament)
        {
            if (tournament == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await managementRepository.createNewTournament(tournament);

            return Created("created", created);
        }
    }
}
