﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using AOS = Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace ETicaretAndroidAPP
{
    static public class DataBase
    {
        static private SQLiteConnection connection;
        static private string sqlPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "ormdemo.db3");
        static public SQLiteConnection CheckConnection()
        {
            if (connection == null)
            {
                connection = new SQLiteConnection(sqlPath);
                // if (connection.Table<CustomerAccount>() == null)
                connection.CreateTable<CustomerAccount>();
            }
            return connection;
        }
        static
             public void CreateItems()
        {
            CheckConnection();
            CustomerAccount customerAccount = new CustomerAccount();
            customerAccount.UserName = "softsam";
            customerAccount.Password = "softsam";
            customerAccount.EMail = "softsam@softsam.com";
            customerAccount.PhoneNumber = "54865487966";
            if (connection.Table<CustomerAccount>().ToList().Count == 0)
                connection.Insert(customerAccount);
        }
    }
}