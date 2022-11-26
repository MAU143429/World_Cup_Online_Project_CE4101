using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public interface IAccountRepository
    {
        Task<int> addAccountGroup(GroupWEB ta);
        Task<int> createNewAccount(AccountWEB account);
        Task<int> createNewGroup(GroupWEB group);
        Task<List<AccountWEB>> getAccountByNickname(string nick);
        Task<List<GroupWEB>> getGroupById(string GId);
        Task<List<GroupWEB>> getGroupsByNE(string nickname, string email);
        Task<List<AccountWEB>> getInformationAccountByEmail(string email);
        Task<AccountWEB> getLoginAccountWEB(string login);
        Task<bool> getRoleAccountByEmail(string email);
        Task<List<Tournament_Account_SWEB>> getScoreByGroupId(string GId);
        Task<List<Tournament_Account_SWEB>> getScoreByTournamentId(string tId);
        Task<bool> isAccountInGroup(string tId, string nickname, string email);
    }
}