using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ETicaretAndroidAPP.Model;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "ProductShowActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ProductShowActivity : AppCompatActivity
    {
        LinearLayout commentLayout=null;
        protected override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.product_show);
            // Create your application here
            
       
            //FindViewById<TextView>(Resource.Id.lblProductName).Text = GeneralInfo.currentProduct.ProductName;
            //FindViewById<TextView>(Resource.Id.lblAvaliability).Text = GeneralInfo.currentProduct.ProductCount > 0 ? "In Stock" : "Not in stock";
            //FindViewById<TextView>(Resource.Id.lblPrice).Text = GeneralInfo.currentProduct.ProductPrice.ToString() + " $";
            //FindViewById<TextView>(Resource.Id.lblProductID).Text = GeneralInfo.currentProduct.ProductID;
            //FindViewById<ImageView>(Resource.Id.imgProductImage).SetImageDrawable(GetDrawable(GeneralInfo.currentProduct.ProductImage));
            //commentLayout = FindViewById<LinearLayout>(Resource.Id.linearCommentLayout);
            //commentLayout.FindViewById<TextView>(Resource.Id.lblCommentUserName).Text = GeneralInfo.customerAccount.UserName;
            //commentLayout.FindViewById<TextView>(Resource.Id.lblCommentPlus).Text = "125";
            //commentLayout.FindViewById<TextView>(Resource.Id.lblCommentNegative).Text = "70";
            //commentLayout.FindViewById<TextView>(Resource.Id.txtUserComment).Text = "Ürün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesinÜrün Çok İyi ne diyecesin";
        }

 
        private void ProductShowActivity_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, ((Button)sender).Tag.ToString(), ToastLength.Short).Show();
        }
    }
}