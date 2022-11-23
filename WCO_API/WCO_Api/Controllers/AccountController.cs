
using Microsoft.AspNetCore.Mvc;
using WCO_Api.Repository;
using WCO_Api.WEBModels;
namespace WCO_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /* <summary>
    * Class <c>AccountController</c> Controlador de propiedad Account que 
    * maneja sus transacciones con la base de datos.
    * </summary>
    */
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository = new();


        /* <summary>
        * Method <c>createAccount</c> método que se comunica con el repositorio, el cual 
        * hace la petición a la propiedad AccountDatabase para hacer la inserción de la cuenta a WCO database.
        * </summary>
        */

        [HttpPost("AddAccount")]
        public async Task<IActionResult> createAccount([FromBody] AccountWEB account)
        {

            if (account == null)
                return BadRequest("null input");
            Task<AccountWEB>? accountInDB = accountRepository.getLoginAccountWEB(account.email);
            if (accountInDB.Result != null)
                return BadRequest("Account already exists");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdA = await accountRepository.createNewAccount(account);

            if (createdA == 1)
            {
                return Created("api/Account/AddAccount", "Se creo un usuario exitosamente");
            }
            else
            {
                return StatusCode(500, "Error al crear un usuario, intente de nuevo mas tarde");
            }

        }

        /*
         * Recibe un objeto LoginAccountWEB, se hacen las operaciones necesarias para que se meta la información a la base de datos.
         * Retorna un Task<Bool> que indica si la opracion tuvo exito o no
         */
        [HttpPost("AddGroup")]
        public async Task<IActionResult> createGroup(GroupWEB group) 
        {
            if (group == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdG = await accountRepository.createNewGroup(group);

            if (createdG == 1)
            {
                return Created("api/Account/AddGroup", "Se creo un grupo exitosamente");
            }
            else
            {
                return StatusCode(500, "Error al crear un grupo, intente de nuevo mas tarde");
            }

        }

        [HttpPost("AddAccountToGroup")]
        public async Task<IActionResult> addAccountGroup(Tournament_Account_SWEB ta)
        {
            if (ta == null)
                return BadRequest("null input");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Ver si alguien quiere meterse a un grupo en el que ya está


            var createdG = await accountRepository.addAccountGroup(ta);

            if (createdG == 1)
            {
                return Created("api/Account/AddGroup", "Se creo un grupo exitosamente");
            }
            else
            {
                return StatusCode(500, "Error al crear un grupo, intente de nuevo mas tarde");
            }

        }

        [HttpPost("Login")]
        public async Task<bool> loginAccount([FromBody] LoginAccountWEB loginAccount)
        {
            
            Task<AccountWEB>? accountInDB = accountRepository.getLoginAccountWEB(loginAccount.email);

                
            if (accountInDB.Result.password != loginAccount.password)
                return false;

            return true;

        }

        /*
         * Recibe un string nick, se hacen las operaciones necesarias para que retorne una lista que contiene un objeto AccountWEB
         * Retorna un Task<List<AccountWEB>> que utilizará la página web
         */
        [HttpGet("GetGroupById/{GId}")]
        public async Task<List<GroupWEB>> getGroupById(string GId)
        {

            return await accountRepository.getGroupById(GId);

        }

        [HttpGet("GetScoreByGroupId/{GId}")]
        public async Task<List<Tournament_Account_SWEB>> getScoreByGroupId(string GId)
        {

            return await accountRepository.getScoreByGroupId(GId);

        }

        [HttpGet("GetAccountByNickname/{nick}")]
        public async Task<List<AccountWEB>> getAccountByNickname(string nick)
        {

            return await accountRepository.getAccountByNickname(nick);

        }

        /*
         * Recibe un string email, se hacen las operaciones necesarias para que retorne una lista que contiene un objeto AccountWEB
         * Retorna un Task<List<AccountWEB>> con la información necesaria que utilizará la página web
         */

        [HttpGet("GetInformationAccountByEmail/{email}")]
        public async Task<List<AccountWEB>> getInformationAccountByEmail(string email)
        {

            return await accountRepository.getInformationAccountByEmail(email);

        }

        /*
         * Recibe un string email, se hacen las operaciones necesarias para indicar el rol de un usuario según su email
         * Retorna un Task<bool> indicando si la cuenta es de administrador o no
         */

        [HttpGet("Role/{email}")]
        public async Task<bool> getRoleAccountByEmail(string email)
        {

            return await accountRepository.getRoleAccountByEmail(email);

        }

    }
}
