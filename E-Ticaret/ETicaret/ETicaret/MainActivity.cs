using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Views.InputMethods;
using Android.Views;

namespace ETicaret
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtUserName, txtPassword;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ImageView image = FindViewById<ImageView>(Resource.Id.imgShoppingBasket);
            image.SetImageResource(Resource.Drawable.shoppingBasket);

            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;
            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPwd);
            #region Enter Key pressed in edit texts

            txtUserName.KeyPress += (sender, e) =>
            {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    txtPassword.RequestFocus();
                    //your logic here
                    e.Handled = true;
                }
            };
            txtPassword.KeyPress += (sender, e) =>
            {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {

                    //your logic here
                    btnLogin.PerformClick();
                    e.Handled = true;
                }
            };
            #endregion

        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            //Make validations and connect to database and check is this user is exists and infos are correct
            if (ValidUserName(txtUserName.Text) && ValidPassWord(txtPassword.Text))
            {
                //this is a test validation
                //email is valid
                MessageBox("Valid email and password");
            }
            else
            {
                MessageBox("Invalid email or password");
            }
        }
        private void MessageBox(string text)
        {
            Toast.MakeText(this, text, ToastLength.Short).Show();
        }

        private bool ValidPassWord(string text)
        {
            return text.Length >= 8;
        }

        private bool ValidUserName(string text)
        {
            return text.Contains("@");
        }
    }
}