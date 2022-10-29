using WCO_Api.Models;
using WCO_Api.WEBModels;

/**
 * Repositorio donde se centralizan las peticiones a querys de la base de datos, esto para tener orden en esto
*/

namespace WCO_Api.Data
{
    public class ManagementRepository
    {

        SQLDB sQLDB = new SQLDB();

        public async Task<int> createNewTournament(Tournament tournament)
        {
            return await sQLDB.insertTournament(tournament);
        }
        
        public async Task<int> createNewBracket(Bracket bracket)
        {
            return await sQLDB.insertBracket(bracket);
        }

        public async Task<int> createNewMatch(MatchWEB match)
        {
            return await sQLDB.insertMatch(match);
        }

        public async Task<int> updateTeamId(Team team)
        {
            return await sQLDB.updateTeam(team);
        }
        
        public async Task<IEnumerable<Tournament>> getTournaments()
        {
            return await sQLDB.getTournaments();
        }

        public async Task<IEnumerable<Tournament>> getTournamentsById(string id)
        {
            return await sQLDB.getTournamentsById(id);
        }

        public async Task<IEnumerable<TeamWEB>> getTeamByTournamentId(string id)
        {
            return await sQLDB.getTeamByTournamentId(id);
        }

        public async Task<IEnumerable<BracketWEB>> getBracketsByTournamentId(string id)
        {
            return await sQLDB.getBracketsByTournamentId(id);
        }

        public async Task<IEnumerable<MatchOut>> getMatchesByBracketId(int id)
        {
            return await sQLDB.getMatchesByBracketId(id);
        }

        public async Task<IEnumerable<TeamWEB>> getTeamsByMatchId(int id)
        {
            return await sQLDB.getTeamsByMatchId(id);
        }

        public async Task<IEnumerable<TeamWEB>> getTeamsByType(string type)
        {
            return await sQLDB.getTeamsByType(type);
        }

        public async Task<int> getTotalBrackets()
        {
            return await sQLDB.getTotalBrackets();
        }

        public async Task<int> getTotalMatches()
        {
            return await sQLDB.getTotalMatches();
        }

    }
}
