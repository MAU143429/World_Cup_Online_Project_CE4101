using Microsoft.Data.SqlClient;
using WCO_Api.Logic;
using WCO_Api.WEBModels;

namespace WCO_Api.Database
{
    /* <summary>
    /// Class <c>AccountDatabase</c> propiedad AccountDatabase que realiza las operaciones
    /// asociadas a Account con WCO DB.
    /// </summary>
    /// */
    public class AccountDatabase
    {
        string CONNECTION_STRING;

        MyIdGenerator myIdGenerator = new();

        public AccountDatabase()
        {
            CONNECTION_STRING = "Data Source=localhost;Initial Catalog=WCODB;Integrated Security=True";
        }

        /* <summary>
        /// Method <c>insertAccount</c> método que se comunica con WCO DB para realizar la inserción 
        /// de una nueva cuenta.
        /// </summary>
        */
        public async Task<int> insertAccount(AccountWEB account)
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

                string query =
                          $"INSERT INTO [dbo].[Account] ([nickname], [email], [name], [lastName], [birthdate], [country], [password], [isAdmin])" +
                          $"VALUES ('{account.nickname}', '{account.email}', '{account.name}', '{account.lastname}', '{account.birthdate}', " +
                          $"'{account.country}', '{account.password}', '{account.isAdmin}');";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }

        }

        /*
         * Query para obtener una cuenta respecto a su email
         */
        public async Task<int> insertGroup(GroupWEB group)
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

                //Se crea una llave para el grupo
                string groupId = myIdGenerator.GetUUID();
                groupId = group.tId + groupId;

                string query =
                          $"INSERT INTO [dbo].[Group] ([g_id], [name], [tournament_id])" +
                          $"VALUES ('{groupId}', '{group.name}', '{group.tId}' )";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                string query2 =
                          $"UPDATE [dbo].[Tournament_Account_S]" +
                          $"SET [group_id] = '{groupId}'" +
                          $"WHERE [acc_nick] = '{group.acc_nick}' and [acc_email] = '{group.acc_email}' ";

                command = new SqlCommand(query2, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }

        }

        public async Task<int> insertAccountGroup(GroupWEB ta)
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

                string query =
                          $"UPDATE [dbo].[Tournament_Account_S]" +
                          $"SET [group_id] = '{ta.GId}'" +
                          $"WHERE [acc_nick] = '{ta.acc_nick}' and [acc_email] = '{ta.acc_email}' and [t_id] = '{ta.GId.Substring(0,6)}'";

                command = new SqlCommand(query, myConnection);

                //assosiate the command-variable with the transaction
                command.Transaction = transaction;
                //Se inserta a la tabla torneos el torneo en sí
                command.ExecuteNonQuery();

                transaction.Commit();

                return 1;
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.WriteLine(error);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }

        }

        public async Task<List<GroupWEB?>> getGroupById(string inputGId)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Group]" +
                $"WHERE g_id = '{inputGId}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            SqlDataReader reader = sqlCmd.ExecuteReader();

            List<GroupWEB> groupL = new();

            while (reader.Read())
            {
                GroupWEB group = new();

                group.GId = reader.GetValue(0).ToString();
                group.name = reader.GetValue(1).ToString();
                group.tId = reader.GetValue(2).ToString();

                groupL.Add(group);
            }
            
            myConnection.Close();

            return groupL;

        }

        public async Task<List<GroupWEB?>> getGroupsByNE(string nickname, string email)
        {
            SqlConnection myConnection = new();
            SqlDataReader reader = null;

            myConnection.ConnectionString = CONNECTION_STRING;

            myConnection.Open();

            //Obtener lista con los codigos de grupo de un usuario

            string query = $"SELECT group_id " +
                $"FROM [dbo].[Tournament_Account_S]" +
                $"WHERE acc_nick = '{nickname}' and acc_email = '{email}';";

            SqlCommand sqlCmd = new(query, myConnection);

            reader = sqlCmd.ExecuteReader();

            List<string> groupsIds = new List<string>();

            while (reader.Read())
            {
                groupsIds.Add(reader.GetValue(0).ToString());
            }

            reader.Close();

            List<GroupWEB> groupL = new();

            foreach (var inputGId in groupsIds)
            {

                string query2 = $"SELECT * " +
                $"FROM [dbo].[Group]" +
                $"WHERE g_id = '{inputGId}';";

                sqlCmd = new(query2, myConnection);

                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    GroupWEB group = new();

                    group.GId = reader.GetValue(0).ToString();
                    group.name = reader.GetValue(1).ToString();
                    group.tId = reader.GetValue(2).ToString();

                    groupL.Add(group);
                }

                reader.Close();

            }

            return groupL;

        }

        public async Task<bool> isAccountInGroup(string tId, string nickname, string email)
        {
            try
            {
                SqlConnection myConnection = new();

                myConnection.ConnectionString = CONNECTION_STRING;

                string query = $"SELECT [group_id] " +
                    $"FROM [dbo].[Tournament_Account_S]" +
                    $"WHERE t_id = '{tId}' and acc_nick = '{nickname}' and acc_email = '{email}'";

                SqlCommand sqlCmd = new(query, myConnection);

                myConnection.Open();

                SqlDataReader reader = sqlCmd.ExecuteReader();

                bool isInGroup = false;


                while (reader.Read())
                {

                    Console.WriteLine(reader.GetValue(0).ToString());
                    //Se encuentra en un grupo
                    if (reader.GetValue(0).ToString().Length != 0)
                    {
                        isInGroup = true;
                    }
                    //Sino no se encuentra en grupo

                }

                myConnection.Close();

                return isInGroup;
            }
            catch (Exception)
            {

                return false;
            }


        }


        public async Task<List<Tournament_Account_SWEB?>> getScoreByGroupId(string inputGId)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Tournament_Account_S]" +
                $"WHERE group_id = '{inputGId}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<Tournament_Account_SWEB> groupScoresL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tournament_Account_SWEB score = new();

                    score.TId = reader.GetValue(0).ToString();
                    score.acc_nick = reader.GetValue(1).ToString();
                    score.acc_email = reader.GetValue(2).ToString();
                    score.points = (float)(double) reader.GetValue(3);
                    score.GId = reader.GetValue(4).ToString();

                    groupScoresL.Add(score);
                }
            }
            myConnection.Close();

            return groupScoresL;

        }

        public async Task<List<Tournament_Account_SWEB?>> getScoreByTournamentId(string inputTId)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Tournament_Account_S]" +
                $"WHERE t_id = '{inputTId}'" +
                $"ORDER BY points DESC;";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<Tournament_Account_SWEB> groupScoresL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tournament_Account_SWEB score = new();

                    score.TId = reader.GetValue(0).ToString();
                    score.acc_nick = reader.GetValue(1).ToString();
                    score.acc_email = reader.GetValue(2).ToString();
                    score.points = (float)(double)reader.GetValue(3);
                    score.GId = reader.GetValue(4).ToString();

                    groupScoresL.Add(score);
                }
            }
            myConnection.Close();

            return groupScoresL;

        }

        public async Task<AccountWEB?> getAccountByEmail(string inputEmail)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{inputEmail}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();
            AccountWEB? fromResponse = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    fromResponse.password = reader.GetValue(6).ToString();
                    fromResponse.email = reader.GetValue(0).ToString();
                }
                else
                {
                    fromResponse = null;
                }
            }
            myConnection.Close();

            return fromResponse;

        }

        /*
         * Query para obtener una cuenta respecto a su nickname
         */

        public async Task<List<AccountWEB?>> getAccountByNickname(string nick)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE nickname = '{nick}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<AccountWEB> accountL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    AccountWEB account = new();

                    account.nickname = reader.GetValue(0).ToString();
                    account.email = reader.GetValue(1).ToString();
                    account.name = reader.GetValue(2).ToString();
                    account.lastname = reader.GetValue(3).ToString();
                    account.birthdate = reader.GetValue(4).ToString();
                    account.country = reader.GetValue(5).ToString();
                    account.password = reader.GetValue(6).ToString();

                    accountL.Add(account);
                }

            }
            myConnection.Close();

            return accountL;
        }

        /*
         * Query para obtener la información de una cuenta respecto a su email
         */

        public async Task<List<AccountWEB>> getInformationAccountByEmail(string email)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT * " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{email}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            List<AccountWEB> accountL = new();

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    AccountWEB account = new();

                    account.nickname = reader.GetValue(0).ToString();
                    account.email = reader.GetValue(1).ToString();
                    account.name = reader.GetValue(2).ToString();
                    account.lastname = reader.GetValue(3).ToString();
                    account.birthdate = reader.GetValue(4).ToString();
                    account.country = reader.GetValue(5).ToString();
                    account.password = reader.GetValue(6).ToString();

                    accountL.Add(account);
                }

            }
            myConnection.Close();

            return accountL;
        }

        /*
         * Query para obtener el rol de una cuenta respecto a su email
         */

        public async Task<bool> getRoleAccountByEmail(string email)
        {
            SqlConnection myConnection = new();

            myConnection.ConnectionString = CONNECTION_STRING;

            string query = $"SELECT [isAdmin] " +
                $"FROM [dbo].[Account]" +
                $"WHERE email = '{email}';";

            SqlCommand sqlCmd = new(query, myConnection);

            myConnection.Open();

            //SqlDataReader reader = sqlCmd.ExecuteReader();

            bool accIsAdmin = false;

            using (SqlDataReader reader = sqlCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    accIsAdmin = (bool)reader.GetValue(0);

                }
                
            }

            myConnection.Close();

            return accIsAdmin;

        }
    }
}
