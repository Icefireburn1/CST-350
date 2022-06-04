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
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = CST350; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
    }
}
