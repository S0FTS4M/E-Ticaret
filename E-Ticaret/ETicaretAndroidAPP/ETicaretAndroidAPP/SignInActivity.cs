using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Views.InputMethods;
using Android.Views;
using SQLite;
using ETicaretAndroidAPP.Model;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class SignInActivity : Activity
    {
        EditText txtUserName, txtPassword;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignIn);
            ImageView image = FindViewById<ImageView>(Resource.Id.imgShoppingBasket);
            
            image.SetImageResource(Resource.Drawable.shoppingBasketIcon);
            
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;

            Button btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            btnSignUp.Click += BtnSignUp_Click;
            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPwd);

            CheckBox chcbShowLogin = FindViewById<CheckBox>(Resource.Id.chcbShowLogin);
            chcbShowLogin.CheckedChange += ChcbShowLogin_CheckedChange;
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

        private void ChcbShowLogin_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {

            if (((CheckBox)sender).Checked)
                txtPassword.InputType = Android.Text.InputTypes.TextVariationNormal;
            else
                txtPassword.InputType = Android.Text.InputTypes.TextVariationPassword |
                          Android.Text.InputTypes.ClassText;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }
       
        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            //Make validations and connect to database and check is this user is exists and infos are correct
            if (Validator.ValidUserName(txtUserName.Text) && Validator.ValidPassWord(txtPassword.Text))//
            {
                //this is a test validation
                //email is valid
                SQLiteConnection connection = DataBase.CheckConnection();
                TableQuery<CustomerAccount> customer = connection.Table<CustomerAccount>().Where(x => x.UserName == txtUserName.Text);
                CustomerAccount foundCustomer = null;
                if (customer.Count() > 0 )
                {
                    foundCustomer = customer.First();

                    if (foundCustomer != null && foundCustomer.Password != string.Empty && foundCustomer.Password == txtPassword.Text)
                    {   //anasayfaya yönlendirme
                        CustomerInfo.UserConnected = true;
                        CustomerInfo.FillDatas(txtUserName.Text);
                        StartActivity(typeof(MainActivity));
                        MessageBoxShort("Correct Username and Password");
                    }
                    else
                        MessageBoxShort("Username or Password is wrong!");
                }
                else
                    MessageBoxShort("There is no user found.");
            }
            else
            {
                MessageBoxShort("User name or password was not valid.");
            }
        }
        private void MessageBoxShort(string text)
        {
            Toast.MakeText(this, text, ToastLength.Short).Show();
        }
        private void MessageBoxLong(string text)
        {
            Toast.MakeText(this, text, ToastLength.Long).Show();
        }


       
    }
}