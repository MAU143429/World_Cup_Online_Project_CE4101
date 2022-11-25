using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public interface IMatchRepository
    {
        Task<int> createNewMatch(MatchWEB match);
        Task<IEnumerable<MatchOut>> getMatchById(int id);
        Task<IEnumerable<MatchOut>> getMatchesByBracketId(int id);
        Task<IEnumerable<PlayerWEB>> getPlayersbyBothTeamId(int id1, int id2);
        Task<IEnumerable<PlayerWEB>> getPlayersbyTeamId(int id);
        Task<IEnumerable<TeamWEB>> getTeamsByMatchId(int id);
    }
}