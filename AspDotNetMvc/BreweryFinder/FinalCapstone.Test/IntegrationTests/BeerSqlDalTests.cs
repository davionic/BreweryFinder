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
    public class BeerSqlDalTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=BreweryFinder;Integrated Security=True";
        private int id;
        private int id2;

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
                id2 = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetBeersTest()
        {
            BeerSqlDal dal = new BeerSqlDal(connectionString);
            List<Beer> beers = dal.GetBeers(id);

            Assert.IsNotNull(beers);
            Assert.AreEqual(3, beers.Count);
        }

        [TestMethod]
        public void GetBeerTest()
        {
            BeerSqlDal dal = new BeerSqlDal(connectionString);
            Beer beer = dal.GetBeer(id2);

            Assert.AreEqual(id2, beer.ID);
            Assert.AreEqual("Quadruple Mega IPA", beer.Name);
        }

        [TestMethod]
        public void AddBeerTest()
        {
            BeerSqlDal dal = new BeerSqlDal(connectionString);
            Beer beer = new Beer()
            {
                BreweryID = id,
                Name = "Literally Blended Hops",
                Type = "IPA",
                ABV = 3,
                IBU = 100,
                HintsOf = "Bavaria",
                Description = "Feel superior to other IPA drinkers by cutting out the middleman and going straight to the source. Blended for drinkability."
            };

            Assert.IsTrue(dal.AddBeer(beer));
        }

        [TestMethod]
        public void UpdateBeerTest()
        {
            BeerSqlDal dal = new BeerSqlDal(connectionString);
            Beer beer = new Beer()
            {
                ID = id2,
                BreweryID = id,
                Name = "Literally Blended Hops",
                Type = "IPA",
                ABV = 3,
                IBU = 100,
                HintsOf = "Bavaria",
                Description = "Feel superior to other IPA drinkers by cutting out the middleman and going straight to the source. Blended for drinkability."
            };

            Assert.IsTrue(dal.UpdateBeer(beer));
        }

        [TestMethod]
        public void DeleteBeerTest()
        {
            BeerSqlDal dal = new BeerSqlDal(connectionString);

            Assert.IsTrue(dal.DeleteBeer(id2));
        }
    }
}
