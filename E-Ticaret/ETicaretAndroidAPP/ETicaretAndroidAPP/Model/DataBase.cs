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
using ETicaretAndroidAPP.Model;
using Android.Support.V4.Content;

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
                //creates if it is not exists
                connection.CreateTable<CustomerAccount>();
                connection.CreateTable<CustomerPersonal>();
                connection.CreateTable<Product>();
            }
            return connection;
        }
        static public void CreateItems()
        {

            CheckConnection();
            CustomerAccount customerAccount = new CustomerAccount();
            customerAccount.UserName = "softsam";
            customerAccount.Password = "softsam";
            customerAccount.EMail = "softsam@softsam.com";
            customerAccount.PhoneNumber = "54865487966";
            if (connection.Table<CustomerAccount>().ToList().Count == 0)
                connection.Insert(customerAccount);

            CustomerPersonal customerPersonal = new CustomerPersonal();
            customerPersonal.UserName = customerAccount.UserName;
            customerPersonal.Name = "Şamil";
            customerPersonal.Surname = "SOFTSAM";
            customerPersonal.BirthDate = DateTime.Now;
            customerPersonal.Address = "Yaylalarda dagda her yerde yasiyor";
            if (connection.Table<CustomerPersonal>().ToList().Count == 0)
                connection.Insert(customerPersonal);

            List<Product> products = new List<Product>();
            Product product = new Product();
            product.ProductID = "12235465";
            product.ProductName = "Apple";
            product.ProductPrice = 2000;
            product.ProductCount = 3;
            product.ProductCategory = ProductCategory.Man.ToString();
            product.ProductType = ProductType.Sport.ToString();
            product.ProductImage = Resource.Drawable.ic_man_casual_shoe;
            products.Add(product);

            product = new Product();
            product.ProductID = "12235466";
            product.ProductName = "Android";
            product.ProductPrice = 1000;
            product.ProductCount = 8;
            product.ProductCategory = ProductCategory.Women.ToString();
            product.ProductType = ProductType.Sport.ToString();
            product.ProductImage = Resource.Drawable.ic_woman_casual_shoe;
            products.Add(product);

            product = new Product();
            product.ProductID = "12235467";
            product.ProductName = "Windows";
            product.ProductPrice = 2500;
            product.ProductCount = 100;
            product.ProductCategory = ProductCategory.Women.ToString();
            product.ProductType = ProductType.Sport.ToString();
            product.ProductImage = Resource.Drawable.ic_winter_boots;
            products.Add(product);

            product = new Product();
            product.ProductID = "12235468";
            product.ProductName = "Nike";
            product.ProductPrice = 250;
            product.ProductCount = 75;
            product.ProductCategory = ProductCategory.Kids.ToString();
            product.ProductType = ProductType.Sport.ToString();
            product.ProductImage = Resource.Drawable.ic_menu_gallery;
            products.Add(product);


            product = new Product();
            product.ProductID = "12235469";
            product.ProductName = "Adidas";
            product.ProductPrice = 300;
            product.ProductCount = 175;
            product.ProductCategory = ProductCategory.Man.ToString();
            product.ProductType = ProductType.Sport.ToString();
            product.ProductImage = Resource.Drawable.ic_menu_camera;

            product = new Product();
            product.ProductID = "12235470";
            product.ProductName = "Adidas";
            product.ProductPrice = 300;
            product.ProductCount = 175;
            product.ProductCategory = ProductCategory.Women.ToString();
            product.ProductType = ProductType.Casual.ToString();
            product.ProductImage = Resource.Drawable.ic_menu_home;
            products.Add(product);
            if (connection.Table<Product>().ToList().Count == 0)
                connection.InsertAll(products);
        }
    }
}