using WCO_Api.Database;
using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public class MatchRepository : IMatchRepository
    {

        MatchDatabase sQLDB = new MatchDatabase();

        public async Task<int> createNewMatch(MatchWEB match)
        {
            return await sQLDB.insertMatch(match);
        }

        public async Task<IEnumerable<MatchOut>> getMatchesByBracketId(int id)
        {
            return await sQLDB.getMatchesByBracketId(id);
        }

        public async Task<IEnumerable<TeamWEB>> getTeamsByMatchId(int id)
        {
            return await sQLDB.getTeamsByMatchId(id);
        }

        public async Task<IEnumerable<MatchOut>> getMatchById(int id)
        {
            return await sQLDB.getMatchById(id);
        }

        public async Task<IEnumerable<PlayerWEB>> getPlayersbyTeamId(int id)
        {
            return await sQLDB.getPlayersbyTeamId(id);
        }

        public async Task<IEnumerable<PlayerWEB>> getPlayersbyBothTeamId(int id1, int id2)
        {
            return await sQLDB.getPlayersbyBothTeamId(id1, id2);
        }



    }
}
