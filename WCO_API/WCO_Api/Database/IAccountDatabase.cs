using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public interface IAccountDatabase
    {
        Task<AccountWEB?> getAccountByEmail(string inputEmail);
        Task<List<AccountWEB?>> getAccountByNickname(string nick);
        Task<List<AccountWEB>> getInformationAccountByEmail(string email);
        Task<bool> getRoleAccountByEmail(string email);
        Task<int> insertAccount(AccountWEB account);
    }
}