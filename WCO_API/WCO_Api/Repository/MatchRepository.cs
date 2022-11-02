using WCO_Api.Database;
using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public class MatchRepository
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

        public async Task<int> getTotalMatches()
        {
            return await sQLDB.getTotalMatches();
        }

    }
}
