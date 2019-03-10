using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Extensions;

namespace FinalCapstone.Controllers
{
    public class BaseController : Controller
    {
        public void SetBreweryId(int breweryId)
        {
            HttpContext.Session.Set("Brewery_ID", breweryId);
        }

        public void SetUser(string userType)
        {
            HttpContext.Session.Set("User_Type", userType);
        }

        public int GetBreweryId()
        {
            int result = HttpContext.Session.Get<int>("Brewery_ID");
            return result;
        }

        public bool ConfirmBrewer(int breweryId)
        {
            bool result = HttpContext.Session.Get<int>("Brewery_ID") == breweryId;
            return result;
        }

        public bool GetIsAdminBoolean()
        {
            string result = HttpContext.Session.Get<string>("User_Type");

            return (result == "Admin");
        }

        public bool GetIsBrewerBoolean()
        {
            string result = HttpContext.Session.Get<string>("User_Type");

            return (result == "Brewer");
        }

        public void Logout()
        {
            HttpContext.Session.Clear();
        }
    }
}