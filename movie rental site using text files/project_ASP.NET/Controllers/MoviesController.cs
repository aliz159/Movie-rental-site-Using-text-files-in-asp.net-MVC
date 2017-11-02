using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.Models;
using project_ASP.NET.Logic;

namespace project_ASP.NET.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            List<Movie> list = MovieHelper.GetMoviesList();
            return View(list);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string movieName, string Category)
        {
            if (movieName == "" && Category == "")
            {
                TempData["error"] = "You must fill in all the fields!!";
                return View("New");
            }
            else if (movieName == "")
            {
                TempData["errorName"] = "Please fill in the movie name!!";
                return View("New");
            }
            else if (Category == "")
            {
                TempData["errorCategory"] = "Please fill in the movie category!";
                return View("New");
            }
            else
            {
                MovieHelper.WriteNewMovie(movieName, Category);
                return RedirectToAction("Index");
            }
        }            
    }
}