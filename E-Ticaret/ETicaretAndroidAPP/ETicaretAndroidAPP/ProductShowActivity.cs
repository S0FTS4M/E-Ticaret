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
    [Activity(Label = "ProductShowActivity")]
    public class ProductShowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product_show);
            // Create your application here
           
            FindViewById<TextView>(Resource.Id.lblProductName).Text = GeneralInfo.currentProduct.ProductName;
            FindViewById<TextView>(Resource.Id.lblAvaliability).Text = GeneralInfo.currentProduct.ProductCount > 0 ? "In Stock" : "Not in stock";
            FindViewById<TextView>(Resource.Id.lblPrice).Text = GeneralInfo.currentProduct.ProductPrice.ToString() + " $";
            FindViewById<TextView>(Resource.Id.lblProductID).Text = GeneralInfo.currentProduct.ProductID;
            FindViewById<ImageView>(Resource.Id.imgProductImage).SetImageDrawable(GetDrawable(GeneralInfo.currentProduct.ProductImage));

        }
    }
}