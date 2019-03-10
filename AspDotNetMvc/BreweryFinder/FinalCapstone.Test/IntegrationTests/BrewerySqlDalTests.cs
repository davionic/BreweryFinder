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
    public class BrewerySqlDalTests
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

                cmd = new SqlCommand("SELECT TOP 1 breweryID FROM breweries;" , conn);
                id = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetBreweriesTest()
        {
            BrewerySqlDal dal = new BrewerySqlDal(connectionString);
            List<Brewery> breweries = dal.GetBreweries();

            Assert.IsNotNull(breweries);
            Assert.AreEqual(2, breweries.Count);
        }

        [TestMethod]
        public void GetBreweryTest()
        {
            BrewerySqlDal dal = new BrewerySqlDal(connectionString);
            Brewery brewery = dal.GetBrewery(id);

            Assert.AreEqual(id, brewery.ID);
            Assert.AreEqual("IPA Land", brewery.Name);
        }

        [TestMethod]
        public void AddBreweryTest()
        {
            BrewerySqlDal dal = new BrewerySqlDal(connectionString);
            Brewery brewery = new Brewery()
            {
                Name = "Giannis Antetokounmpo Brewery",
                Address = "The Mecca",
                City = "Milwaukee",
                State = "WI",
                PhoneNumber = "543 295 5933",
                Hours = "noon-midnight",
                Description = "The Greek Freak knows his beer."
            };

            Assert.IsTrue(dal.AddBrewery(brewery));
        }

        [TestMethod]
        public void UpdateBreweryTest()
        {
            BrewerySqlDal dal = new BrewerySqlDal(connectionString);
            Brewery brewery = new Brewery()
            {
                ID = id,
                Name = "Giannis Antetokounmpo Brewery",
                Address = "The Mecca",
                City = "Milwaukee",
                State = "WI",
                PhoneNumber = "543 295 5933",
                Hours = "noon-midnight",
                Description = "The Greek Freak knows his beer."
            };

            Assert.IsTrue(dal.UpdateBrewery(brewery));
        }

        [TestMethod]
        public void DeleteBreweryTest()
        {
            BrewerySqlDal dal = new BrewerySqlDal(connectionString);

            Assert.IsTrue(dal.DeleteBrewery(id));
        }
    }
}
