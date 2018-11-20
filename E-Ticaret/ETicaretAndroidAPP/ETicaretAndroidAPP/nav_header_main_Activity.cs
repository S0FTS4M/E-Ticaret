using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ETicaretAndroidAPP.Model;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "nav_header_main_Activity")]
    public class nav_header_main_Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.nav_header_main);
            // Create your application here
            //TextView lblName=FindViewById<TextView>(Resource.Id.lblUserName);
            //TextView lblemail = FindViewById<TextView>(Resource.Id.lblUserEmail);
            //if(CustomerInfo.UserConnected)
            //{
            //    lblName.Text = CustomerInfo.customerAccount.UserName;
            //    lblemail.Text = CustomerInfo.customerAccount.EMail;
            //    Toast.MakeText(this, "User connected...", ToastLength.Short);
            //}
            //else
            //    Toast.MakeText(this, "User did not connected...", ToastLength.Short);


        }
    }
}