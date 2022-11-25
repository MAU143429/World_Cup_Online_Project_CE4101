using WCO_Api.WEBModels;

namespace WCO_Api.Repository
{
    public interface ITournamentRepository
    {
        Task<int> createNewTournament(TournamentWEB tournament);
        Task<IEnumerable<BracketWEB>> getBracketsByTournamentId(string id);
        Task<IEnumerable<TeamWEB>> getTeamByTournamentId(string id);
        Task<IEnumerable<TeamWEB>> getTeamsByType(string type);
        Task<IEnumerable<TournamentOut>> getTournaments();
        Task<IEnumerable<TournamentOut>> getTournamentsById(string id);
    }
}