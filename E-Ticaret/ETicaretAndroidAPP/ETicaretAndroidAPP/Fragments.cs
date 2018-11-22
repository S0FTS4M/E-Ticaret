using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using ETicaretAndroidAPP.Model;

namespace ETicaretAndroidAPP
{
    public class AccountInfoFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.accountInfo, null);
            view.FindViewById<TextInputEditText>(Resource.Id.txtaccUserName).Text = CustomerInfo.customerAccount.UserName;

            view.FindViewById<TextInputEditText>(Resource.Id.txtaccEmail).Text = CustomerInfo.customerAccount.EMail;
            view.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone).Text = CustomerInfo.customerAccount.PhoneNumber;

            return view;
        }
    }
    public class PersonalInfoFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
    public class ContentFragment : Fragment
    {
        int position;
        public static ContentFragment NewInstace(int pos)
        {
            var f = new ContentFragment();
            var b = new Bundle();
            b.PutInt("position", pos);
            f.Arguments = b;
            return f;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            int inflateLayout = 0;
            if (position == 0)
                inflateLayout = Resource.Layout.accountInfo;
            else if (position == 1)
                inflateLayout = Resource.Layout.personalInfo;
            var root = inflater.Inflate(inflateLayout, container, false);

            setViewComp(root,position);
            ViewCompat.SetElevation(root, 50);

            return root;
        }

        private void setViewComp(View root,int position)
        {
            if(position == 0)
            {
                //fill account info here.
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccUserName).Text = CustomerInfo.customerAccount.UserName;
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccEmail).Text = CustomerInfo.customerAccount.EMail;
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone).Text = CustomerInfo.customerAccount.PhoneNumber;
            }
            else if(position == 1)
            {
                //fill personal info here
            }
        }
    }
}