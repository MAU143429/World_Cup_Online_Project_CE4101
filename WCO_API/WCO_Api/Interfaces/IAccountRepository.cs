using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public interface IAccountRepository
    {
        Task<int> createNewAccount(AccountWEB account);
        Task<List<AccountWEB>> getAccountByNickname(string nick);
        Task<List<AccountWEB>> getInformationAccountByEmail(string email);
        Task<AccountWEB> getLoginAccountWEB(string login);
        Task<bool> getRoleAccountByEmail(string email);
    }
}