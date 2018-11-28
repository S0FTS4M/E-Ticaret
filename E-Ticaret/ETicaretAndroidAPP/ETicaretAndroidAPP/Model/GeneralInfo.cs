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
using SQLite;

namespace ETicaretAndroidAPP.Model
{
    static class GeneralInfo
    {
        public static bool UserConnected = false;
        public static CustomerAccount customerAccount = null;
        public static CustomerPersonal customerPersonal = null;
        public static Product currentProduct;

        static public void FillDatas(string username)
        {
            SQLiteConnection connection = DataBase.CheckConnection();
            //query the username for customer account
            TableQuery<CustomerAccount> customerAccountQuery = connection.Table<CustomerAccount>().Where(x => x.UserName == username);
            CustomerAccount _customerAccount = customerAccountQuery.First();
            if (_customerAccount != null)
                customerAccount = _customerAccount;

            //query the user name for customer personal info
            TableQuery<CustomerPersonal> customerPersonalQuery = connection.Table<CustomerPersonal>().Where(x => x.UserName == username);
            CustomerPersonal _customerPersonal = customerPersonalQuery.First();
            if (_customerAccount != null)
                customerPersonal = _customerPersonal;
            UserConnected = true;
        }
    }
}