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

        public async Task<int> insertTournament(TournamentWEB newTournament)
        {

            SqlTransaction transaction = null;
            SqlConnection myConnection = null;
            SqlCommand command = null;

            try
            {

                myConnection = new SqlConnection(CONNECTION_STRING);

                myConnection.Open();

                //Start the transaction
                transaction = myConnection.BeginTransaction();

                string query = $"INSERT INTO [dbo].[Tournament] ([to_id], [name], [startDate], [endDate], [description], [type])" +
                          $"VALUES ('{newTournament.ToId}', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                /*
                //Para que muera la vara

                string queryE = $"INSERT INTO [dbo].[Tournament] ([to_id], [name], [startDate], [endDate], [description], [type])" +
                          $"VALUES ('{newTournament.ToId}', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";

                command = new SqlCommand(queryE, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;

                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();
                */

                //Crear los brackets asociados al torneo

                foreach (var bracketName in newTournament.brackets)
                {
                    string query2 = $"INSERT INTO [dbo].[Bracket] ( [name], [tournamentId])" +
                          $"VALUES ('{bracketName}', '{newTournament.ToId}');";

                    command = new SqlCommand(query2, myConnection);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                }

                //Update a equipos registrados a un torneo

                foreach (var teamId in newTournament.teams)
                {
                    string query3 =
                          $"UPDATE [dbo].[Team]" +
                          $"SET [tournamentId] = '{newTournament.ToId}'" +
                          $"WHERE [te_id] = {teamId}";

                    command = new SqlCommand(query3, myConnection);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                }

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally {
                myConnection.Close();
            }


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
