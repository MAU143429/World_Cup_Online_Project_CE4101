using WCO_Api.Database;
using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    /// <summary>
    /// Class <c>AccountRepository</c> propiedad que instancia las peticiones requeridas por 
    /// el controlador de Account para comunicarse con WCO DB
    /// </summary>
    public class AccountRepository
    {
        AccountDatabase sQLDB = new ();

        /// <summary>
        /// Method <c>createNewAccount</c> método que se comunica con la propiedad AccountDatabase para realizar la inserción 
        /// de una nueva cuenta.
        /// </summary>
        public async Task<int> createNewAccount(AccountWEB account)
        {
            return await sQLDB.insertAccount(account);
        }

        public async Task<int> createNewGroup(GroupWEB group)
        {
            return await sQLDB.insertGroup(group);
        }

        public async Task<int> addAccountGroup(Tournament_Account_SWEB ta)
        {
            return await sQLDB.insertAccountGroup(ta);
        }

        public async Task<List<GroupWEB>> getGroupById(string GId)
        {
            return await sQLDB.getGroupById(GId);
        }

        public async Task<List<Tournament_Account_SWEB>> getScoreByGroupId(string GId)
        {
            return await sQLDB.getScoreByGroupId(GId);
        }

        public async Task<AccountWEB> getLoginAccountWEB (string login)
        {
            return await sQLDB.getAccountByEmail(login);
        }

        public async Task<List<AccountWEB>> getAccountByNickname(string nick) 
        {
            return await sQLDB.getAccountByNickname(nick);
        }

        public async Task<List<AccountWEB>> getInformationAccountByEmail(string email)
        {
            return await sQLDB.getInformationAccountByEmail(email);
        }

        public async Task<bool> getRoleAccountByEmail(string email)
        {
            return await sQLDB.getRoleAccountByEmail(email);
        }

        
    }
}
