using E_Ticaret.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class SignInUp
    {
        public CustomerAccount SignUp { get; set; }
        public Sign_In SignIn { get; set; }
    }
    public class Sign_In
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}