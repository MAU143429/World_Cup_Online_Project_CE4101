using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WCO_Api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {

        [HttpPost]
        
        public async Task<ActionResult<Match>> Post(Match match)
        {
            return match;
        }

    }
}
