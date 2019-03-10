using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public class BeerSqlDal : IBeerSqlDal
    {
        private readonly string _connectionString;

        public BeerSqlDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddBeer(Beer beer)
        {
            string sql = "INSERT INTO beers (breweryID, name, ABV, IBU, style, hintsOf, description) VALUES (@breweryId, @name, @abv, @ibu, @style, @hintsOf, @description);";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@breweryId", beer.BreweryID);
                    cmd.Parameters.AddWithValue("@name", beer.Name);
                    cmd.Parameters.AddWithValue("@abv", beer.ABV);
                    cmd.Parameters.AddWithValue("@ibu", beer.IBU);
                    cmd.Parameters.AddWithValue("@style", beer.Type);
                    cmd.Parameters.AddWithValue("@hintsOf", beer.HintsOf);
                    cmd.Parameters.AddWithValue("@description", beer.Description);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

            return rows > 0;
        }

        public bool DeleteBeer(int beerId)
        {
            string sql = "DELETE FROM reviews WHERE beerID = @id; ";
            sql += "DELETE FROM beers WHERE beerID = @id;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", beerId);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }

            return rows == 1;
        }

        public Beer GetBeer(int beerId)
        {
            Beer beer = new Beer();
            string sql = @"SELECT * FROM beers WHERE beerID = @beerId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@beerId", beerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {                        
                        beer.ID = Convert.ToInt32(reader["beerID"]);
                        beer.BreweryID = Convert.ToInt32(reader["breweryID"]);
                        beer.Name = Convert.ToString(reader["name"]);
                        beer.Type = Convert.ToString(reader["style"]);
                        beer.ABV = Convert.ToDouble(reader["ABV"]);
                        beer.IBU = Convert.ToInt32(reader["IBU"]);
                        beer.Description = Convert.ToString(reader["description"]);
                        beer.HintsOf = Convert.ToString(reader["hintsOf"]);                    
                    }
                }
            }

            catch (Exception ex)
            {

            }

            return beer;
        }

        public List<Beer> GetBeers(int breweryId)
        {
            List<Beer> beers = new List<Beer>();
            string sql = @"SELECT * FROM beers WHERE breweryID = @breweryId";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@breweryId", breweryId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Beer beer = new Beer
                        {
                            ID = Convert.ToInt32(reader["beerID"]),
                            BreweryID = Convert.ToInt32(reader["breweryID"]),
                            Name = Convert.ToString(reader["name"]),
                            Type = Convert.ToString(reader["style"]),
                            ABV = Convert.ToDouble(reader["ABV"]),
                            IBU = Convert.ToInt32(reader["IBU"]),
                            Description = Convert.ToString(reader["description"]),
                            HintsOf = Convert.ToString(reader["hintsOf"])
                        };
                        beers.Add(beer);
                    }
                }
            }

            catch (Exception ex)
            {

            }

            return beers;
        }

        public bool UpdateBeer(Beer beer)
        {
            string sql = "UPDATE beers SET breweryID=@breweryId, name=@name, ABV=@abv, IBU=@ibu, style=@style, hintsOf=@hintsOf, description=@description WHERE beerID = @id;";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", beer.ID);
                    cmd.Parameters.AddWithValue("@breweryId", beer.BreweryID);
                    cmd.Parameters.AddWithValue("@name", beer.Name);
                    cmd.Parameters.AddWithValue("@abv", beer.ABV);
                    cmd.Parameters.AddWithValue("@ibu", beer.IBU);
                    cmd.Parameters.AddWithValue("@style", beer.Type);
                    cmd.Parameters.AddWithValue("@hintsOf", beer.HintsOf);
                    cmd.Parameters.AddWithValue("@description", beer.Description);
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
