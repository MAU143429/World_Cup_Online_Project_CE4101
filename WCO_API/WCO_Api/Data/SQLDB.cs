using Microsoft.AspNetCore.Mvc;
using WCO_Api.Models;
using WCO_Api.WEBModels;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Data.SqlClient;
using WCO_Api.Loaders;

namespace WCO_Api.Data
{
    public class SQLDB
    {
        private const String CONNECTION_STRING = "server=MSI;database=WCODB;trusted_connection=true;";
        private readonly SqlConnection _Connection;
        public SQLDB()
        {
            _Connection = new SqlConnection(CONNECTION_STRING);
        }

        public async Task CreateTournament(TournamentWEB newTournament)
        {
            String queryString =
                        $"INSERT INTO [dbo].[Tournament] ([id], [name], [startDate], [endDate], [description], [type])" +
                        $"VALUES ('5T43GF', '{newTournament.Name}', '{newTournament.StartDate}', '{newTournament.EndDate}', '{newTournament.Description}' , '{newTournament.Type}');";


            SqlCommand command = new SqlCommand(newTournament.ToPostQuery(), _Connection);

            await _Connection.OpenAsync();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await _Connection.CloseAsync();
        }









    }
}




