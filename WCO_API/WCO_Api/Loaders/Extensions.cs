using WCO_Api.Models;
using WCO_Api.WEBModels;



namespace WCO_Api.Loaders{
    public static class Extensions
    {
        public static String ToPostQuery(this TournamentWEB newTournament)
        {
            return $"INSERT INTO [dbo].[Tournament] ([id], [name], [startDate], [endDate], [description], [type])" +
                   $"VALUES ('5T43GF', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";

        }

    }
}
