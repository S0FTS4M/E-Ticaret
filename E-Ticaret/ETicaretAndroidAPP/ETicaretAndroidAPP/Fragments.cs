using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using ETicaretAndroidAPP.Model;
using SQLite;

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
            view.FindViewById<TextInputEditText>(Resource.Id.txtaccUserName).Text = GeneralInfo.customerAccount.UserName;

            view.FindViewById<TextInputEditText>(Resource.Id.txtaccEmail).Text = GeneralInfo.customerAccount.EMail;
            view.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone).Text = GeneralInfo.customerAccount.PhoneNumber;

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

            setViewComp(root, position);
            ViewCompat.SetElevation(root, 50);

            return root;
        }
        //which layout we are currently on
        // View currentLayout;
        private void setViewComp(View root, int position)
        {
            if (position == 0)
            {
                //fill account info here.
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccUserName).Text = GeneralInfo.customerAccount.UserName;
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccEmail).Text = GeneralInfo.customerAccount.EMail;
                root.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone).Text = GeneralInfo.customerAccount.PhoneNumber;

                //find button and set click event as UPDATE
                root.FindViewById<Button>(Resource.Id.btnaccUpdate).Click += btnAccUpdate_Click;

            }
            else if (position == 1)
            {
                //fill personal info here
                root.FindViewById<TextInputEditText>(Resource.Id.txtpersName).Text = GeneralInfo.customerPersonal.Name;
                root.FindViewById<TextInputEditText>(Resource.Id.txtpersSurname).Text = GeneralInfo.customerPersonal.Surname;
                root.FindViewById<EditText>(Resource.Id.txtpersAddress).Text = GeneralInfo.customerPersonal.Address;
                root.FindViewById<TextInputEditText>(Resource.Id.txtpersBirthDate).Text = GeneralInfo.customerPersonal.BirthDate.ToShortDateString();

                //find button and set click event as UPDATE
            }
            // currentLayout = root;
        }

        private void btnAccUpdate_Click(object sender, EventArgs e)
        {
            View root = (View)((Button)sender).Parent;
            bool updateSuccess = true;
            TextInputEditText txtoldPwd = root.FindViewById<TextInputEditText>(Resource.Id.txtaccOldPwd);
            TextInputEditText txtnewPwd= root.FindViewById<TextInputEditText>(Resource.Id.txtaccPwd);
            TextInputEditText txtPwdConfirm = root.FindViewById<TextInputEditText>(Resource.Id.txtaccPwdConfirm);
            TextInputEditText txtemail= root.FindViewById<TextInputEditText>(Resource.Id.txtaccEmail);
            TextInputEditText txtPhone= root.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone);

            ResetColorsOnAccountInfoTextBoxes(root, txtemail, txtnewPwd, txtoldPwd, txtPhone, txtPwdConfirm);
            //check current password
            string currentPwd = txtoldPwd.Text;
            if (currentPwd == GeneralInfo.customerAccount.Password)
            {
                string newPwd = txtnewPwd.Text;
                //if user wants to change pwd so he/she needs to give us valid pwd
                if (newPwd.Length > 0 && Validator.ValidPassWord(newPwd))
                {
                    //if pwd is valid then check confirmation pwd
                    string confirmPwd =txtPwdConfirm.Text;
                    if (newPwd == confirmPwd)
                    {
                        //we can Make password Update here
                        GeneralInfo.customerAccount.Password = newPwd;
                    }
                    else
                    {
                        //confirmation value is not equal to pwd
                        txtPwdConfirm.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.redfornotvalid);
                        updateSuccess = false;
                        Toast.MakeText(((View)root.Parent).Context, "Password Confirmation is not equal to Password", ToastLength.Short).Show();
                    }
                }
                else if (newPwd.Length > 0 && Validator.ValidPassWord(newPwd) == false)
                {
                    updateSuccess = false;
                   txtnewPwd.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.redfornotvalid);
                    Toast.MakeText(((View)root.Parent).Context, "New Password is not a valid password.", ToastLength.Short).Show();
                }
                string email = txtemail.Text;
                if (Validator.ValidEmail(email))
                {
                    GeneralInfo.customerAccount.EMail = email;
                }
                else
                {
                    updateSuccess = false;
                    txtemail.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.redfornotvalid);
                    Toast.MakeText(((View)root.Parent).Context, "New Email is not valid.", ToastLength.Short).Show();
                }
                string phone = root.FindViewById<TextInputEditText>(Resource.Id.txtaccPhone).Text;
                if (Validator.ValidPhone(phone, 11))
                {
                    GeneralInfo.customerAccount.PhoneNumber = phone;
                }
                else
                {
                    updateSuccess = false;
                    txtPhone.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.redfornotvalid);
                    Toast.MakeText(((View)root.Parent).Context, "New Phone number is not valid.", ToastLength.Short).Show();
                }
            }
            else
            {
                updateSuccess = false;
                txtoldPwd.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.redfornotvalid);
                Toast.MakeText(((View)root.Parent).Context, "Your password is not correct.", ToastLength.Short).Show();
            }
            if (updateSuccess)
            {
                SQLiteConnection connection = DataBase.CheckConnection();
                connection.Update(GeneralInfo.customerAccount);
                //give a message
                // var runnable = new Java.Lang.Runnable(() =>);
                Toast.MakeText(((View)root.Parent).Context, "Update Successfull", ToastLength.Short).Show();
                //((Android.App.Activity)root.Context).RunOnUiThread(runnable);
                //runnable.Run();

                ResetColorsOnAccountInfoTextBoxes(root,txtemail,txtnewPwd,txtoldPwd,txtPhone,txtPwdConfirm);
            }
        }

        private void ResetColorsOnAccountInfoTextBoxes(View root, TextInputEditText txtemail, TextInputEditText txtnewPwd, TextInputEditText txtoldPwd, TextInputEditText txtPhone, TextInputEditText txtPwdConfirm)
        {
            txtemail.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.blueforui);
            txtnewPwd.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.blueforui);
            txtoldPwd.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.blueforui);
            txtPhone.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.blueforui);
            txtPwdConfirm.BackgroundTintList = ContextCompat.GetColorStateList(root.Context, Resource.Color.blueforui);
        }
    }
}