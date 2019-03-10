using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalCapstone.Models;
using FinalCapstone.Dal;
using Microsoft.AspNetCore.Http;
using FinalCapstone.Extensions;

namespace FinalCapstone.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBrewerySqlDal _breweryDal;
        private readonly IBeerSqlDal _beerDal;
        private readonly IReviewSqlDal _reviewDal;
        private readonly IUserSqlDal _userDal;

        public HomeController(IBrewerySqlDal breweryDal, IBeerSqlDal beerDal, IReviewSqlDal reviewDal, IUserSqlDal userDal)
        {
            _breweryDal = breweryDal;
            _beerDal = beerDal;
            _reviewDal = reviewDal;
            _userDal = userDal;
        }

        public IActionResult Index()
        {
            BreweryListViewModel model = new BreweryListViewModel();
            model.Breweries = _breweryDal.GetBreweries();
            model.IsAdminLoggedIn = GetIsAdminBoolean();
            model.IsBrewerLoggedIn = GetIsBrewerBoolean();
            return View(model);
        }

        public IActionResult BreweryDetail(int breweryId)
        {

            BreweryDetailViewModel model = new BreweryDetailViewModel();
            model.ThisBrewery = _breweryDal.GetBrewery(breweryId);
            model.ThisBrewery.Beers = _beerDal.GetBeers(model.ThisBrewery.ID);
            int breweryIden = GetBreweryId();
            model.IsBrewerLoggedIn = breweryIden == model.ThisBrewery.ID;
            model.IsAdminLoggedIn = GetIsAdminBoolean();
          
            foreach (Beer beer in model.ThisBrewery.Beers)
            {
                beer.AverageRating = _reviewDal.GetAverageRating(beer.ID);
            }
            return View(model);
        }

        public IActionResult BeerDetail(int beerId)
        {
            BeerDetailViewModel model = new BeerDetailViewModel();
            model.ThisBeer = _beerDal.GetBeer(beerId);
            model.ThisBeer.Reviews = _reviewDal.GetReviews(model.ThisBeer.ID);
            model.ThisBeer.AverageRating = _reviewDal.GetAverageRating(beerId);
            int breweryIden = GetBreweryId();
            model.IsBrewerLoggedIn = breweryIden == model.ThisBeer.BreweryID;
            model.IsAdminLoggedIn = GetIsAdminBoolean();
            return View(model);
        }

        public IActionResult NewReview(int beerId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BeerDetail(NewReviewViewModel model)
        {
            var newReview = new Review
            {
                BeerID = model.BeerID,
                User = model.User,
                Subject = model.Subject,
                BodyText = model.BodyText,
                Rating = model.Rating
            };
            _reviewDal.NewReview(newReview);
            return RedirectToAction("BeerDetail", new { BeerID = model.BeerID });
        }

        public IActionResult NewBrewery()
        {
            if (!GetIsAdminBoolean())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(NewBreweryViewModel model)
        {
                Brewery newBrewery = new Brewery
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PhoneNumber = model.PhoneNumber,
                    Hours = model.Hours,
                    Description = model.Description
                };
                _breweryDal.AddBrewery(newBrewery);
                return RedirectToAction(nameof(Index));
            
        }

        public IActionResult DeleteBrewery(int breweryId)
        {
            if (!GetIsAdminBoolean())
            {
                return RedirectToAction("Login");
            }
            
            _breweryDal.DeleteBrewery(breweryId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BreweryUpdate(BreweryDetailViewModel model)
        {
            _breweryDal.UpdateBrewery(model.ThisBrewery);
            return RedirectToAction("BreweryDetail", new { BreweryID = model.ThisBrewery.ID });
        }

        public IActionResult UpdateBrewery(BreweryDetailViewModel model)
        {
            if (!GetIsAdminBoolean() && !ConfirmBrewer(model.ThisBrewery.ID))
            {
                return RedirectToAction("Login");
            }
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BeerUpdate(BeerDetailViewModel model)
        {
            _beerDal.UpdateBeer(model.ThisBeer);
            return RedirectToAction("BeerDetail", new { BeerID = model.ThisBeer.ID });
        }

        public IActionResult UpdateBeer(BeerDetailViewModel model)
        {
            if (!ConfirmBrewer(model.ThisBeer.BreweryID))
            {
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult AddBeer(int breweryId)
        {
            if (!GetIsAdminBoolean() && !ConfirmBrewer(breweryId))
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BreweryDetail(NewBeerViewModel model)
        {
            var newBeer = new Beer
            {
                BreweryID = model.BreweryID,
                Name = model.Name,
                Type = model.Type,
                ABV = model.ABV,
                IBU = model.IBU,
                HintsOf = model.HintsOf,
                Description = model.Description,
            };
            _beerDal.AddBeer(newBeer);
            return RedirectToAction("BreweryDetail", new { BreweryID = model.BreweryID });
        }

        [HttpPost]
        public IActionResult DeleteBeer(BreweryDetailViewModel model)
        {
            _beerDal.DeleteBeer(model.BeerID);
            return RedirectToAction("BreweryDetail", new { BreweryID = model.ThisBrewery.ID });
        }

        [HttpPost]
        public IActionResult AddBrewer(NewBrewerViewModel model)
        {
            User user = new User()
            {
                BreweryID = model.BreweryID,
                Name = model.Name,
                Password = model.Password
            };
            _userDal.NewBrewer(user);
            return RedirectToAction("BreweryDetail", new {model.BreweryID });
        }

        [HttpPost]
        public IActionResult NewBrewer(int breweryId)
        {
            if (!GetIsAdminBoolean())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            model.PopulateSelectList(_breweryDal.GetBreweries());
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            User user = new User()
            {
                Name = model.Username,
                Password = model.Password,                
            };
            if (!model.IsAdmin)
            {
                user.BreweryID = model.BreweryID;
            };

            if (model.IsAdmin)
            {
                if (_userDal.ValidateAdmin(user))
                {
                    SetUser("Admin");
                }
                else if (!_userDal.ValidateAdmin(user))
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                    return View("Login", model);
                }
            }
            else
            {
                if (_userDal.ValidateUser(user))
                {
                    SetUser("Brewer");
                    SetBreweryId(model.BreweryID);
                    int testInt = GetBreweryId();
                }
                else if (!_userDal.ValidateUser(user))
                {
                    ModelState.AddModelError("invalid-credentials", "An invalid username or password was provided");
                     return View("Login", model);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserLogout()
        {
            Logout();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
