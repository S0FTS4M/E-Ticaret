using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaretv2.Models
{
    public static class CustomAuth
    {
        public static FirebaseClient firebase = new FirebaseClient("https://eticaretreact.firebaseio.com/");
        public static FirebaseAuth UserAuth = new FirebaseAuth();
    }
}