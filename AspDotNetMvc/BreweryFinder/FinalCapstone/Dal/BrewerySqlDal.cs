using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public class BrewerySqlDal : IBrewerySqlDal
    {
        private readonly string connectionString;

        public BrewerySqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddBrewery(Brewery brewery)
        {
            string sql = "INSERT INTO breweries (name, address, city, state, phoneNumber, hours, description) VALUES (@name, @address, @city, @state, @phone, @hours, @description);";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", brewery.Name);
                    cmd.Parameters.AddWithValue("@address", brewery.Address);
                    cmd.Parameters.AddWithValue("@city", brewery.City);
                    cmd.Parameters.AddWithValue("@state", brewery.State);
                    cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                    cmd.Parameters.AddWithValue("@hours", brewery.Hours);
                    cmd.Parameters.AddWithValue("@description", brewery.Description);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }

            return rows > 0;
        }

        public List<Brewery> GetBreweries()
        {
            List<Brewery> breweries = new List<Brewery>();
            string sql = @"SELECT * FROM breweries;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Brewery brewery = new Brewery()
                        {
                            ID = Convert.ToInt32(reader["breweryID"]),
                            Name = Convert.ToString(reader["name"]),
                            Address = Convert.ToString(reader["address"]),
                            City = Convert.ToString(reader["city"]),
                            State = Convert.ToString(reader["state"]),
                            PhoneNumber = Convert.ToString(reader["phoneNumber"]),
                            Hours = Convert.ToString(reader["hours"]),
                            Description = Convert.ToString(reader["description"]),
                            Beers = new List<Beer>()
                        };
                        breweries.Add(brewery);
                    }
                }
            }

            catch (Exception ex)
            {

            }

            return breweries;
        }

        public Brewery GetBrewery(int breweryId)
        {
            Brewery brewery = new Brewery();
            string sql = @"SELECT * FROM breweries WHERE breweryID = @breweryId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@breweryId", breweryId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        brewery = new Brewery()
                        {
                            ID = Convert.ToInt32(reader["breweryID"]),
                            Name = Convert.ToString(reader["name"]),
                            Address = Convert.ToString(reader["address"]),
                            City = Convert.ToString(reader["city"]),
                            State = Convert.ToString(reader["state"]),
                            PhoneNumber = Convert.ToString(reader["phoneNumber"]),
                            Hours = Convert.ToString(reader["hours"]),
                            Description = Convert.ToString(reader["description"]),
                            Beers = new List<Beer>()
                        };
                    }
                }
            }

            catch (Exception ex)
            {
                
            }

            return brewery;
        }

        public bool UpdateBrewery(Brewery brewery)
        {
            string sql = "UPDATE breweries SET name=@name, address=@address, city=@city, state=@state, phoneNumber=@phone, hours=@hours, description=@description WHERE breweryID = @id;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", brewery.ID);
                    cmd.Parameters.AddWithValue("@name", brewery.Name);
                    cmd.Parameters.AddWithValue("@address", brewery.Address);
                    cmd.Parameters.AddWithValue("@city", brewery.City);
                    cmd.Parameters.AddWithValue("@state", brewery.State);
                    cmd.Parameters.AddWithValue("@phone", brewery.PhoneNumber);
                    cmd.Parameters.AddWithValue("@hours", brewery.Hours);
                    cmd.Parameters.AddWithValue("@description", brewery.Description);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }

            return rows == 1;
        }

        public bool DeleteBrewery(int breweryId)
        {
            string sql = "DELETE FROM reviews WHERE beerID IN (SELECT beerID FROM beers WHERE breweryID = @id); ";
            sql += "DELETE FROM beers WHERE breweryID = @id; ";
            sql += "DELETE FROM user_brewer WHERE breweryID = @id; ";
            sql += "DELETE FROM breweries WHERE breweryID = @id;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", breweryId);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

            return rows > 0;
        }
    }
}
