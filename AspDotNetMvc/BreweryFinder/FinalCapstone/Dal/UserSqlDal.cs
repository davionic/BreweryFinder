using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public class UserSqlDal : IUserSqlDal
    {
        private readonly string _connectionString;

        public UserSqlDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool NewBrewer(User user)
        {
            string sql = "INSERT INTO user_brewer (breweryID, userName, password) VALUES (@breweryId, @username, @password);";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@breweryId", user.BreweryID);
                    cmd.Parameters.AddWithValue("@username", user.Name);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

            return rows > 0;
        }

        public bool ValidateUser(User user)
        {
            string sql = "SELECT * FROM user_brewer WHERE breweryID = @breweryId AND userName = @name AND password = @password;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@breweryId", user.BreweryID);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    rows = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

            }

            return rows > 0;
        }

        public bool ValidateAdmin(User user)
        {
            string sql = "SELECT COUNT(*) FROM user_admin WHERE userName = @name AND password = @password;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    rows = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

            }

            return rows > 0;
        }

        public bool DeleteBrewer(int brewerId)
        {
            string sql = "DELETE FROM user_brewer WHERE brewerID = @id;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", brewerId);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

            return rows == 1;
        }
    }
}
