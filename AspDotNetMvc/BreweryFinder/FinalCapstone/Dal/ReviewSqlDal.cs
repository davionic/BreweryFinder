using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public class ReviewSqlDal : IReviewSqlDal
    {
        private readonly string connectionString;

        public ReviewSqlDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public double GetAverageRating(int beerId)
        {
            double averageRating = 0.0;
            string sql = @"SELECT AVG(rating) FROM reviews WHERE beerID = @beerId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@beerId", beerId);

                    averageRating = (int)cmd.ExecuteScalar();
                }
            }

            catch (Exception ex)
            {
                
            }

            return averageRating;
        }

        public List<Review> GetReviews(int beerId)
        {
            List<Review> reviews = new List<Review>();
            string sql = @"SELECT * FROM reviews WHERE beerID = @beerId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@beerId", beerId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Review review = new Review()
                        {
                            ID = Convert.ToInt32(reader["reviewID"]),
                            BeerID = Convert.ToInt32(reader["beerID"]),
                            User = Convert.ToString(reader["userName"]),
                            Subject = Convert.ToString(reader["subject"]),
                            BodyText = Convert.ToString(reader["reviewBody"]),
                            Rating = Convert.ToInt32(reader["rating"])
                        };
                        reviews.Add(review);
                    }
                }
            }

            catch (Exception ex)
            {
                
            }

            return reviews;
        }

        public bool NewReview(Review review)
        {
            string sql = "INSERT INTO reviews (beerID, subject, userName, reviewBody, rating) VALUES (@beerId, @subject, @user, @body, @rating);";

            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@beerId", review.BeerID);
                    cmd.Parameters.AddWithValue("@user", review.User);
                    cmd.Parameters.AddWithValue("@subject", review.Subject);
                    cmd.Parameters.AddWithValue("@body", review.BodyText);
                    cmd.Parameters.AddWithValue("@rating", review.Rating);
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
