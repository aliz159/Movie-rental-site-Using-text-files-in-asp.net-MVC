using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_ASP.NET.Models;
using project_ASP.NET.Logic;

namespace project_ASP.NET.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            List<Customer> list = CustomerHelper.GetCustomersList();

            return View(list);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string userName, string subscription, string userAge)
        {
            if (userName == "" && subscription == "" && userAge == "")
            {
                TempData["error"] = "You must fill in all the fields!!";
                TempData["errorName"] = ""; TempData["errorSub"] = ""; TempData["erroruserAge"] = "";
                return View("New");
            }
            else if (userName == "" && subscription == "")
            {
                TempData["errorName"] = "Please fill in your name!!";
                TempData["errorSub"] = "Please fill in the subscription!";
                TempData["erroruserAge"] = "";
                return View("New");
            }
            else if (userName == "" && userAge == "")
            {
                TempData["errorName"] = "Please fill in movie name!!";
                TempData["errorSub"] = "";
                TempData["erroruserAge"] = "Please fill in your userAge!";
                return View("New");
            }
            else if (subscription == "" && userAge == "")
            {
                TempData["errorName"] = "";
                TempData["errorSub"] = "Please fill in the subscription!";
                TempData["erroruserAge"] = "Please fill in your userAge!";
                return View("New");
            }
            else if (userName == "")
            {
                TempData["errorName"] = "Please fill in movie name!!";
                TempData["errorSub"] = ""; TempData["erroruserAge"] = "";
                return View("New");
            }
            else if (userAge == "")
            {
                TempData["erroruserAge"] = "Please fill in your userAge!";
                TempData["errorName"] = ""; TempData["errorSub"] = "";
                return View("New");
            }
            else if (subscription == "")
            {
                TempData["errorSub"] = "Please fill in the subscription!";
                TempData["errorName"] = ""; TempData["erroruserAge"] = "";
                return View("New");
            }
            else
            {
                if (int.Parse(userAge) < 18)
                {
                    TempData["erroruserAge"] = "to rent age must be over 17";
                    return View("New");
                }
                else
                {
                    if (CustomerHelper.DoseCustomerExists(userName))
                    {
                        TempData["errorName"] = "Name already exist, try another one";
                        return View("New");
                    }
                    else
                    {
                        CustomerHelper.WriteNewCustomer(userName, subscription, userAge);
                        return RedirectToAction("Index");
                    }
                }

            }
        }
    }
}