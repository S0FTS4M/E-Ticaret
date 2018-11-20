using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using SQLite;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "SignUp", Theme = "@style/AppTheme")]
    public class SignUpActivity : Activity
    {
        EditText txtUserName;
        EditText txtPassword;
        EditText txtPasswordConfirm;
        EditText txtEmail;
        EditText txtPhone;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignUp);

            ImageView imageView = FindViewById<ImageView>(Resource.Id.imgAddUser);
            imageView.SetImageResource(Resource.Drawable.addUserIcon);

            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtUserName.TextChanged += EditText_TextChanged;

            txtPassword = FindViewById<EditText>(Resource.Id.txtPwd);
            txtPassword.TextChanged += EditText_TextChanged;
            CheckBox checkBoxPWD = FindViewById<CheckBox>(Resource.Id.chcbShow);
            checkBoxPWD.Tag = "pwd";

            txtPasswordConfirm = FindViewById<EditText>(Resource.Id.txtConfirmPwd);
            txtPasswordConfirm.TextChanged += txtPasswordConfirm_TextChanged;
            CheckBox checkBoxPWDConfirm = FindViewById<CheckBox>(Resource.Id.chcbConfirmShow);
            checkBoxPWDConfirm.Tag = "confirm";

            checkBoxPWD.CheckedChange += CheckBox_CheckedChange;
            checkBoxPWDConfirm.CheckedChange += CheckBox_CheckedChange;

            txtEmail = FindViewById<EditText>(Resource.Id.txtemail);
            txtEmail.TextChanged += EditText_TextChanged;

            txtPassword = FindViewById<EditText>(Resource.Id.txtPwd);
            txtPassword.TextChanged += EditText_TextChanged;



            txtPhone = FindViewById<EditText>(Resource.Id.txtPhone);
            txtPhone.TextChanged += EditText_TextChanged;

            Button btnSignup = FindViewById<Button>(Resource.Id.btnSave);
            btnSignup.Click += BtnSignup_Click;
        }

        private void txtPasswordConfirm_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPasswordConfirm.Text == txtPassword.Text)
                ((EditText)sender).BackgroundTintList = GetColorStateList(Resource.Color.greenforshopcart);
            else
                ((EditText)sender).BackgroundTintList = GetColorStateList(Resource.Color.redfornotvalid);
        }

        private void CheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            EditText currentEditText = ((CheckBox)sender).Tag.ToString() == "pwd" ? txtPassword : txtPasswordConfirm;
            if (((CheckBox)sender).Checked)
                currentEditText.InputType = Android.Text.InputTypes.TextVariationNormal;
            else
                currentEditText.InputType = Android.Text.InputTypes.TextVariationPassword |
                          Android.Text.InputTypes.ClassText;

        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ((EditText)sender).SetTextColor(Color.Black);
            ((EditText)sender).BackgroundTintList = GetColorStateList(Resource.Color.greenforshopcart);
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            //if any info is wrong or not valid then we can make the tint color red
            //USERNAME
            //PASSWORD
            //EMAIL
            //PHONE NUMBER
            //Check if valid
            string notValidMessage = "";
            List<EditText> notValidList = new List<EditText>();
            if (Validator.ValidUserName(txtUserName.Text) == false)
            {
                //check database if this name has taken
                notValidList.Add(txtUserName);
                notValidMessage += "UserName ";
            }
            if (Validator.ValidPassWord(txtPassword.Text) == false)
            {
                notValidList.Add(txtPassword);
                notValidMessage += "Password ";

            }
            if (Validator.ValidEmail(txtEmail.Text) == false)
            {
                //check if this email already taken
                notValidList.Add(txtEmail);
                notValidMessage += "Email ";

            }
            if (Validator.ValidPhone(txtPhone.Text, 11) == false)
            {
                notValidList.Add(txtPhone);
                notValidMessage += "Phone ";

            }
            foreach (EditText item in notValidList)
            {
                item.SetTextColor(Color.Red);
                item.BackgroundTintList = GetColorStateList(Resource.Color.redfornotvalid);
            }
            if (notValidList.Count > 0)
                MessageBoxShort(notValidMessage + " Not Valid");

            bool notValid=false;
            //first we need to check if username or email is exists
            if (isUserNameExists(txtUserName.Text))
            {
                MessageBoxShort("Username has taken by someone else before. try another!");
                notValid = true;
                txtUserName.BackgroundTintList = GetColorStateList(Resource.Color.redfornotvalid);
            }
            if (isEmailExists(txtUserName.Text))
            {
                MessageBoxShort("Email has taken by someone else before. try another!");
                notValid = true;
                txtEmail.BackgroundTintList = GetColorStateList(Resource.Color.redfornotvalid);
            }
            if(notValid)
            {
                return;
            }

            SQLiteConnection db = DataBase.CheckConnection();
            CustomerAccount newCustomerAccount = new CustomerAccount();
            newCustomerAccount.UserName    = txtUserName.Text;
            newCustomerAccount.Password    = txtPassword.Text;
            newCustomerAccount.PhoneNumber = txtPhone.Text;
            newCustomerAccount.EMail       = txtEmail.Text;
            db.Insert(newCustomerAccount);
            MessageBoxShort("Sign up completed.");
            base.OnBackPressed();
            //StartActivity(typeof(SignInActivity));

        }

        private bool isEmailExists(string email)
        {
            SQLiteConnection qLiteConnection = DataBase.CheckConnection();
            var found = qLiteConnection.Table<CustomerAccount>().Where(x => x.EMail == email);
            //be spesific
            if (found.Count() > 0)
                return true;
            else
                return false;
        }

        private bool isUserNameExists(string username)
        {
            SQLiteConnection qLiteConnection = DataBase.CheckConnection();
            var found = qLiteConnection.Table<CustomerAccount>().Where(x => x.UserName == username);
            //be spesific
            if (found.Count() > 0)
                return true;
            else
                return false;
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