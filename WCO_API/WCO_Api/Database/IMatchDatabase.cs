using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public interface IMatchDatabase
    {
        Task<List<MatchOut>> getMatchById(int id);
        Task<List<MatchOut>> getMatchesByBracketId(int id);
        Task<List<PlayerWEB>> getPlayersbyBothTeamId(int id1, int id2);
        Task<List<PlayerWEB>> getPlayersbyTeamId(int id);
        Task<List<TeamWEB>> getTeamsByMatchId(int id);
        Task<int> insertMatch(MatchWEB match);
    }
}