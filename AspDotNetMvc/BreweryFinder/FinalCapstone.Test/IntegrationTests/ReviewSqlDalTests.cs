using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using FinalCapstone.Dal;
using FinalCapstone.Models;

namespace FinalCapstone.Test.IntegrationTests
{
    [TestClass]
    public class ReviewSqlDalTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=BreweryFinder;Integrated Security=True";
        private int id;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("DELETE FROM user_brewer;", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM reviews;", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM beers;", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM breweries;", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO breweries (name, address, city, state, phoneNumber, hours, description) VALUES ('IPA Land', 'Summit', 'Columbus', 'OH', '368-9032', 'noon-midnight', 'beards welcome');", conn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO breweries (name, address, city, state, phoneNumber, hours, description) VALUES ('IPA World', 'Summit', 'Columbus', 'OH', '368-9032', 'noon-midnight', 'Imagine Dragons but a brewery');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 breweryID FROM breweries;", conn);
                id = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO beers (breweryID, name, ABV, IBU, style, hintsOf, description) VALUES (@id, 'Quadruple Mega IPA', 5.5, 90, 'IPA', 'gentrification', 'more bitter than your ex');", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO beers (breweryID, name, ABV, IBU, style, hintsOf, description) VALUES (@id, 'Quintuple Ultra IPA', 5.5, 95, 'IPA', 'gentrification', 'more bitter than ginger');", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO beers (breweryID, name, ABV, IBU, style, hintsOf, description) VALUES (@id, 'Infinity IPA', 5.5, 99, 'IPA', 'gentrification', 'more bitter than your exes ex');", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 beerID FROM beers;", conn);
                id = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO reviews (beerID, subject, userName, reviewBody, rating) VALUES (@id, 'Get Uptown Pilsner Downtown', 'C-Bus drinky', 'Best beer Downtown is Uptown Pilsner!!', 5);", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("INSERT INTO reviews (beerID, subject, userName, reviewBody, rating) VALUES (@id, 'More like Throw-Up(town)', 'IPAistheWay', 'Garbage!! Still trying to get the flat taste out of my mouth', 1);", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetReviewsTest()
        {
            ReviewSqlDal dal = new ReviewSqlDal(connectionString);
            List<Review> reviews = dal.GetReviews(id);

            Assert.IsNotNull(reviews);
            Assert.AreEqual(2, reviews.Count);
        }

        [TestMethod]
        public void GetAverageRatingTest()
        {
            ReviewSqlDal dal = new ReviewSqlDal(connectionString);
            double average = dal.GetAverageRating(id);

            Assert.AreEqual(3, average);
        }

        [TestMethod]
        public void NewReviewTest()
        {
            ReviewSqlDal dal = new ReviewSqlDal(connectionString);
            Review review = new Review()
            {
                BeerID = id,
                User = "PaleAleMale",
                Subject = "It's so bitter...",
                BodyText = "No seriously, don't buy this",
                Rating = 2
            };

            Assert.IsTrue(dal.NewReview(review));
        }
    }
}
