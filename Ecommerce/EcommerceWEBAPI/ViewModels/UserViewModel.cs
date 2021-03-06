using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceWEBAPI.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}