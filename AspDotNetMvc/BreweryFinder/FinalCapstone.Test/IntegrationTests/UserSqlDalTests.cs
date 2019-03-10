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
    public class UserSqlDalTests
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

                cmd = new SqlCommand("SELECT TOP 1 breweryID FROM breweries;", conn);
                id = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO user_brewer (breweryID, userName, password) VALUES (@id, 'Brew Master', 'passBosh');", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT TOP 1 brewerID FROM user_brewer;", conn);
                id2 = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void NewBrewerTest()
        {
            UserSqlDal dal = new UserSqlDal(connectionString);
            User user = new User()
            {
                BreweryID = id,
                Name = "Brew Master",
                Password = "passBosh"
            };

            Assert.IsTrue(dal.NewBrewer(user));
        }

        [TestMethod]
        public void ValidateUserTest()
        {
            UserSqlDal dal = new UserSqlDal(connectionString);
            User user = new User()
            {
                BreweryID = id,
                Name = "Brew Master",
                Password = "passBosh"
            };

            Assert.IsTrue(dal.NewBrewer(user));
        }

        [TestMethod]
        public void DeleteBrewerTest()
        {
            UserSqlDal dal = new UserSqlDal(connectionString);

            Assert.IsTrue(dal.DeleteBrewer(id2));
        }
    }
}
