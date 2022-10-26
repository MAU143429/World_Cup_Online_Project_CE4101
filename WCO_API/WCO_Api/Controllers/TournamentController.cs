using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WCO_Api.Data;
using WCO_Api.Models;
using WCO_Api.WEBModels;

namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly SQLDB _SqlDb;

        public TournamentController(SQLDB sqlDb)
        {
            _SqlDb = sqlDb;
        }

        /**
        // GET: <ActivityController>/user
        [HttpGet("user/{username}")]
        public Task<IEnumerable<ActivityDB>> GetActivities(String username)
            => _SqlDb.GetAllActivitiesUser(username);*/

        // POST: <ActivityController>/user
        [HttpPost("Add")]
        public Task CreateTournement(TournamentWEB newTournament)
            => _SqlDb.CreateTournament(newTournament);
    }
}






        
