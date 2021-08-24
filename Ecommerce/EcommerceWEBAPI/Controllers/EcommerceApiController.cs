using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EcommerceWEBAPI.Models;
using EcommerceWEBAPI.ViewModels;

namespace EcommerceWEBAPI.Controllers
{
    public class EcommerceApiController : ApiController
    {
        private OnlineStoreDataEntities _context = new OnlineStoreDataEntities();


        //[Route("Users/All")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            IList<UserViewModel> users = null;
            using(var x = new OnlineStoreDataEntities())
            {
                users = x.Users.Select(y => new UserViewModel()
                {
                    UserID = y.UserID,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Address = y.Address,
                    Email = y.Email,
                    MobileNumber = y.MobileNumber,
                    Password = y.Password
                }).ToList<UserViewModel>();
            }
            if(users.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }
        }

       

    }

}
