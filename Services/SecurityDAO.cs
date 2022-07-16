using CST350_CLC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CST350_CLC.Services
{
    public class SecurityDAO
    {
        readonly string connectionString = @"Data Source=tcp:cst350-clcdbserver.database.windows.net,1433;Initial Catalog=CST350_CLC_db;User Id=icefireburn1@cst350-clcdbserver;Password=Tt110997";

        public bool CreateUser(UserModel user)
        {
            // assume nothing is found
            bool success = false;

            string sqlStatement = "INSERT INTO dbo.users (first_name,last_name,sex,age,state,email,username,password) VALUES (@FIRST_NAME,@LAST_NAME,@SEX,@AGE,@STATE,@EMAIL,@USERNAME,@PASSWORD)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@FIRST_NAME", System.Data.SqlDbType.NVarChar, 50).Value = user.firstName;
                command.Parameters.Add("@LAST_NAME", System.Data.SqlDbType.NVarChar, 50).Value = user.lastName;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.NVarChar, 50).Value = user.sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.NVarChar, 50).Value = user.age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.NVarChar, 50).Value = user.state;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.NVarChar, 50).Value = user.email;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar, 50).Value = user.username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.NVarChar, 50).Value = user.password;

                // Probably wrong thing to check for when inserting
                try
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.Text;

                    if (command.ExecuteNonQuery() >= 1)
                        success = true;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string msg = "Insert Error:";
                    msg += ex.Message;
                    throw new Exception(msg);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }

        internal bool DeleteGameSave(int id)
        {
            // assume nothing is found
            bool success = false;

            string sqlStatement = "DELETE FROM dbo.games WHERE Id = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.NVarChar, 50).Value = id;

                // Probably wrong thing to check for when inserting
                try
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.Text;

                    if (command.ExecuteNonQuery() >= 1)
                        success = true;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string msg = "Insert Error:";
                    msg += ex.Message;
                    throw new Exception(msg);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }

        internal List<GameSaveModel> GetGameSaves(UserModel user)
        {
            // assume nothing is found
            List<GameSaveModel> saves = new List<GameSaveModel>();

            string sqlStatement = "SELECT * FROM dbo.games WHERE user_Id = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.NVarChar, 50).Value = GetUserID(user);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.HasRows)
                    {
                        // Move to first line
                        reader.Read();

                        saves.Add(new GameSaveModel() { ID = (int)reader[0], GameData = reader[2].ToString(), Date = (DateTime)reader[3], PercentFinished = CellBusinessService.GetPercentFinished(reader[2].ToString())});
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return saves;
        }

        internal UserModel FindUserByUsername(string name)
        {
            // assume nothing is found
            UserModel userInfo = null;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @USERNAME";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar, 50).Value = name;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Move to first line
                        reader.Read();

                        userInfo = new UserModel(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return userInfo;
        }

        public bool CreateGameSave(UserModel user, string gameData)
        {
            // assume nothing is found
            bool success = false;

            string sqlStatement = "INSERT INTO dbo.games (user_Id,game_data,date) VALUES (@USER_ID,@GAME_DATA,@DATE)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USER_ID", System.Data.SqlDbType.NVarChar, 50).Value = GetUserID(user);
                command.Parameters.Add("@GAME_DATA", System.Data.SqlDbType.NVarChar, gameData.Length).Value = gameData;

                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                command.Parameters.Add("@DATE", System.Data.SqlDbType.NVarChar, 50).Value = sqlFormattedDate;


                // Probably wrong thing to check for when inserting
                try
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.Text;

                    if (command.ExecuteNonQuery() >= 1)
                        success = true;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    string msg = "Insert Error:";
                    msg += ex.Message;
                    throw new Exception(msg);
                }
                finally
                {
                    connection.Close();
                }
            }
            return success;
        }

        public UserModel FindUserByNameAndPassword(UserModel user)
        {
            // assume nothing is found
            UserModel userInfo = null;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @USERNAME and password = @PASSWORD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar, 50).Value = user.username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.NVarChar, 50).Value = user.password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Move to first line
                        reader.Read();

                        userInfo = new UserModel(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), (int)reader[4], reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return userInfo;
        }

        public int GetUserID(UserModel user)
        {
            string sqlStatement = "SELECT * FROM dbo.Users WHERE username = @USERNAME";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.NVarChar, 50).Value = user.username;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Move to first line
                        reader.Read();

                        return (int)reader[0];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return -1;
        }

        public List<string> GetGameById(int id)
        {
            string sqlStatement = "SELECT game_data FROM dbo.games WHERE Id = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@ID", System.Data.SqlDbType.NVarChar, 50).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // Move to first line
                        reader.Read();

                        return reader[0].ToString().Split(",").ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }
    }
}
