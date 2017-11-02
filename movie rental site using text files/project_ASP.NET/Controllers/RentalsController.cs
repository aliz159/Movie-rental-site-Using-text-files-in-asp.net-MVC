using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.Models;
using project_ASP.NET.Logic;

namespace project_ASP.NET.Controllers
{
    public class RentalsController : Controller
    {
        string ErrorMsg = "";

        public ActionResult New()
        {
            return View();
        }
    
        public ActionResult ReturnMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string name, string movie)
        {
            ViewBag.ErrorMsg = "";
            ViewBag.success = "";
            bool ExistInFile = RentalHelper.GetRentalList().Exists(x => (x.NameOfcustomer == name && x.NameOfMovie == movie));

            if(ExistInFile)
            {
                RentalHelper.DeleteRentedMovie(name, movie);

                if (RentalHelper.DoesMovieReturned(name, movie))
                {
                    ViewBag.success = "Movie returned successful!";
                }
                else
                {
                    ViewBag.ErrorMsg = "There seems to be a problem, Failed to return movie, try again";
                }
            }
            else
            {
                ViewBag.ErrorMsg = "Oops... The customer didn't rent this movie!";
            }
            return View("ReturnMovie");
        }

        [HttpPost]
        public ActionResult CreateNewRentals(string name, string movie)
        {
            bool boolean = true;

            if (RentalHelper.CountingMovieDuplicates(name, movie) <= MovieHelper.CountingMovieDuplicates(name, movie))
            {
                RentalHelper.WriteNewRental(name, movie);

                if (RentalHelper.DoesMovieReturned(name, movie))
                {
                    ErrorMsg = "There seems to be a problem, the movie rental has not been made, try again";
                    boolean = true;
                }
                else
                {
                    ErrorMsg = "Movie rental successful!";
                    boolean = false;
                }
            }
            var ReqOfRent = new { strMsg = ErrorMsg, Isbool = boolean };
            return Json(ReqOfRent);
        }
    }
}