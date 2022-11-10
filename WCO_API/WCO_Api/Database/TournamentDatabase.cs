using Microsoft.Data.SqlClient;
using WCO_Api.Models;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    public class TournamentDatabase
    {

        string CONNECTION_STRING;

        public TournamentDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        public async Task<int> insertTournament(TournamentOut newTournament)
        {

            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"INSERT INTO [dbo].[Tournament] ([to_id], [name], [startDate], [endDate], [description], [type])" +
                          $"VALUES ('{newTournament.ToId}', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<int> insertBracket(BracketWEB newBracket)
        {

            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query =
                          $"INSERT INTO [dbo].[Bracket] ([name], [tournamentId])" +
                          $"VALUES ('{newBracket.Name}', '{newBracket.TournamentId}');";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<int> updateTeam(TeamOut newTeam)
        {

            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query =
                          $"UPDATE [dbo].[Team]" +
                          $"SET [tournamentId] = '{newTeam.TournamentId}'" +
                          $"WHERE [te_id] = {newTeam.TeId}";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;

        }

        public async Task<List<TournamentOut>> getTournaments()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = "SELECT * FROM [dbo].[Tournament]";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TournamentOut> tournaments = new List<TournamentOut>();

            while (reader.Read())
            {
                TournamentOut tournament = new TournamentOut();

                tournament.ToId = reader.GetValue(0).ToString();
                tournament.Name = reader.GetValue(1).ToString();
                tournament.StartDate = reader.GetValue(2).ToString();
                tournament.EndDate = reader.GetValue(3).ToString();
                tournament.Description = reader.GetValue(4).ToString();
                tournament.Type = reader.GetValue(5).ToString();

                tournaments.Add(tournament);

            }
            myConnection.Close();

            return tournaments;
        }

        public async Task<List<TournamentOut>> getTournamentsById(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                        $"FROM [dbo].[Tournament]" +
                        $"WHERE to_id = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TournamentOut> tournaments = new List<TournamentOut>();

            while (reader.Read())
            {
                TournamentOut tournament = new TournamentOut();

                tournament.ToId = reader.GetValue(0).ToString();
                tournament.Name = reader.GetValue(1).ToString();
                tournament.StartDate = reader.GetValue(2).ToString();
                tournament.EndDate = reader.GetValue(3).ToString();
                tournament.Description = reader.GetValue(4).ToString();
                tournament.Type = reader.GetValue(5).ToString();

                tournaments.Add(tournament);

            }
            myConnection.Close();

            return tournaments;
        }

        public async Task<List<TeamWEB>> getTeamByTournamentId(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [te_id], [name] " +
                $"FROM [dbo].[Team]" +
                $"WHERE tournamentId = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TeamWEB> teams = new List<TeamWEB>();

            while (reader.Read())
            {
                TeamWEB newteam = new TeamWEB();

                newteam.TeId = (int)reader.GetValue(0);
                newteam.Name = reader.GetValue(1).ToString();

                teams.Add(newteam);

            }
            myConnection.Close();

            return teams;
        }

        public async Task<List<BracketWEB>> getBracketsByTournamentId(string id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Bracket]" +
                $"WHERE tournamentId = '{id}'";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<BracketWEB> brackets = new List<BracketWEB>();

            while (reader.Read())
            {
                BracketWEB newbracket = new BracketWEB();

                newbracket.BId = (int)reader.GetValue(0);
                newbracket.Name = reader.GetValue(1).ToString();
                newbracket.TournamentId = reader.GetValue(2).ToString();

                brackets.Add(newbracket);

            }
            myConnection.Close();

            return brackets;
        }

        public async Task<List<TeamWEB>> getTeamsByType(string type)
        {
            Console.WriteLine("GETTEAMSBYTYPE");
            Console.WriteLine(type);

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [te_id], [name] " +
                $"FROM [dbo].[Team]" +
                $"WHERE [type] = '{type}' AND [tournamentId] IS NULL";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<TeamWEB> teams = new List<TeamWEB>();

            while (reader.Read())
            {
                TeamWEB team = new TeamWEB();

                team.TeId = (int)reader.GetValue(0);
                team.Name = reader.GetValue(1).ToString();

                teams.Add(team);

            }

            return teams;
        }

    }
}
