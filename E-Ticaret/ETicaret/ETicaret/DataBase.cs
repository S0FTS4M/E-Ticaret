using System;
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
namespace ETicaret
{
    static public class DataBase
    {
        static private SQLiteConnection connection;
        static private string sqlPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
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

    }
}