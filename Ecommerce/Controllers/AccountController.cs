using Ecommerce.Helper;
using Ecommerce.Models;
using Ecommerce.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class AccountController : Controller
    {
        
        private OnlineStoreDataEntities2   _context = new OnlineStoreDataEntities2();
        // GET: Account
        public ActionResult Registration()
        {
            return View(new User());
        }



        [HttpPost]
        public ActionResult Registration(User user)
        {

            if (ModelState.IsValid)
            {
                var res = _context.Users.Where(x => x.Email == user.Email && x.MobileNumber == user.MobileNumber).FirstOrDefault();
                if (res != null)
                {
                    TempData["ErrorMessage"] = "Either Email or Contact already existed";
                }
                else
                {
                    _context.Users.Add(user);
                    var result = _context.SaveChanges();
                    if (result >= 1)
                    {
                        TempData["Message"] = "Successfully Registration Done";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please COntact the Admin,Might facing some Internal Issues";
                        return View(user);
                    }

                    return View(user);
                }
            }
            return View(user);
        }



        [HttpPost]
        public ActionResult Login(CustomLogIn customLogIn)
        {
            if (ModelState.IsValid)
            {
                var res = _context.Users.Where(x => (x.Email == customLogIn.UserName || x.MobileNumber == customLogIn.UserName) && (x.Password == customLogIn.Password)).FirstOrDefault();
                if (res != null)
                {
                    SessionHelper.Instance.user = res;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    TempData["Message"] = "Invalid Credentials !";
                }



            }
            return View(customLogIn);
        }
        // GET: Login
        public ActionResult Login()
        {
            return View(new CustomLogIn());
        }

        public ActionResult Logout()
        {
            SessionHelper.Instance.user = null;
            return RedirectToAction("Login", "Account");
        }


    }
}