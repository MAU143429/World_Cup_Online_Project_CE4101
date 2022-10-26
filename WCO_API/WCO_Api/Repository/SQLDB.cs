using Microsoft.AspNetCore.Mvc;
using WCO_Api.Models;
using WCO_Api.WEBModels;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WCO_Api.Data
{
    public class SQLDB : ControllerBase
    {
        String CONNECTION_STRING;
        
        public SQLDB()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        public async Task<int> insertTournament(TournamentWEB newTournament)
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

        public async Task<List<Tournament>> getTournaments()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = "SELECT * FROM [dbo].[Tournament]";

            SqlCommand sqlCmd = new SqlCommand(query, myConnection);

            myConnection.Open();

            reader = sqlCmd.ExecuteReader();

            List<Tournament> tournaments = new List<Tournament>();

            while (reader.Read())
            {
                Tournament tournament = new Tournament();

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







    }
}




