using Microsoft.Data.SqlClient;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    /// <summary>
    /// Class <c>AccountDatabase</c> propiedad AccountDatabase que realiza las operaciones
    /// asociadas a Account con WCO DB.
    /// </summary>
    public class AccountDatabase
    {
        string CONNECTION_STRING;

        public AccountDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        /// <summary>
        /// Method <c>insertAccount</c> método que se comunica con WCO DB para realizar la inserción 
        /// de una nueva cuenta.
        /// </summary>
        public async Task<int> insertAccount(AccountWEB account)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query =
                          $"INSERT INTO [dbo].[Account] ([nickname], [email], [name], [lastName], [birthdate], [country], [password])" +
                          $"VALUES ('{account.nickname}', '{account.email}', '{account.name}', '{account.lastname}', '{account.birthdate}', " +
                          $"'{account.country}', '{account.password}');";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();
            var created = sqlCmd.ExecuteNonQuery();
            myConnection.Close();

            return created;
        }

    }
}
