
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Class <c>AccountController</c> Controlador de propiedad Account que 
    /// maneja sus transacciones con la base de datos.
    /// </summary>
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository = new();

        /// <summary>
        /// Method <c>createAccount</c> método que se comunica con el repositorio, el cual 
        /// hace la petición a la propiedad AccountDatabase para hacer la inserción de la cuenta a WCO database.
        /// </summary>
        [HttpPost("AddAccount")]
        public async Task<IActionResult> createAccount([FromBody] AccountWEB account)
        {

            if (account == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await accountRepository.createNewAccount(account);

            return Created("created", created);

        }
    }
}
