using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ETicaretAndroidAPP.Model;
using SQLite;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        GridLayout mainLayout;
        LinearLayout template;
        SQLiteConnection connection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //FOR TESTING PURPOSES
            DataBase.CreateItems();
            CustomerInfo.FillDatas("softsam");

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();



            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            var header = navigationView.GetHeaderView(0);
            TextView lblName = header.FindViewById<TextView>(Resource.Id.lblUserName);
            TextView lblemail = header.FindViewById<TextView>(Resource.Id.lblUserEmail);
            if (CustomerInfo.UserConnected)
            {
                lblName.Text = CustomerInfo.customerAccount.UserName;
                lblemail.Text = CustomerInfo.customerAccount.EMail;
                Toast.MakeText(this, "User connected...", ToastLength.Short);
            }
            else
            {
                lblName.Text = "No User";
                lblemail.Text = "No User";
                Toast.MakeText(this, "User did not connected...", ToastLength.Short);
            }

            navigationView.SetNavigationItemSelectedListener(this);
            CoordinatorLayout appBarMain = FindViewById<CoordinatorLayout>(Resource.Id.appBarMain); ;
            GridLayout gridLayout = appBarMain.FindViewById<RelativeLayout>(Resource.Id.contentMain).FindViewById<GridLayout>(Resource.Id.gridContentInc);
            LinearLayout _template = gridLayout.FindViewById<LinearLayout>(Resource.Id.basicCart);
            mainLayout = gridLayout;
            template = _template;

            connection = DataBase.CheckConnection();
            var products = connection.Table<Product>();
            CreateItems(products);
        }
        void CreateItems(TableQuery<Product> products)
        {
            //clear all views on the grid layout so I can create what I want to create
            mainLayout.RemoveAllViews();
            //read the database and get the image and product id and create all

            for (int i = 0; i < products.Count(); i++)
            {
                LinearLayout linearLayout = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.basicCart, null);

                Button button = linearLayout.FindViewById<Button>(Resource.Id.cartShowButton);
                button.Tag = products.ElementAt(i).ProductID;
                button.Click += Button_Click;
                ImageView image = linearLayout.FindViewById<ImageView>(Resource.Id.cartImage);
                image.SetImageDrawable(GetDrawable(products.ElementAt(i).ProductImage));

                #region No need for now
                //linearLayout.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
                //linearLayout.LayoutParameters.Width = DpToPixel(this, 180);
                //linearLayout.LayoutParameters.Height = DpToPixel(this, 150);

                //linearLayout.Orientation = Orientation.Vertical;
                //ImageView image = View.Inflate(this,)
                //image.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
                //image.LayoutParameters.Width = DpToPixel(this, 154);
                //image.LayoutParameters.Height = DpToPixel(this, 93);
                //image.SetImageResource(Resource.Drawable.sportShoe);
                //image.SetForegroundGravity(GravityFlags.Center);
                //linearLayout.AddView(image);
                //Button button = new Button(this);
                //button.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
                ////set buttons tag to its product id so when we click the button we can find the product that
                ////we are looking for
                //button.LayoutParameters.Width = DpToPixel(this, 90);
                //button.LayoutParameters.Height = DpToPixel(this, 35);
                //button.Text = "VIEW";
                //Drawable img = GetDrawable(Resource.Drawable.search);
                // //img.Wait();

                //img.SetBounds(0, 0, 100, 80);
                //button.SetCompoundDrawables(img, null, null, null);
                //button.CompoundDrawableTintList = GetColorStateList(Resource.Color.white);
                //button.Gravity = GravityFlags.CenterHorizontal;
                //button.SetBackgroundColor(Android.Graphics.Color.ParseColor("#FFAA66CC"));
                //button.TextAlignment = TextAlignment.Center;

                //button.SetTextColor(GetColorStateList(Resource.Color.white));
                //button.Tag = i;
                //button.Click += Button_Click;
                //linearLayout.AddView(button);


                //gridLayout.Wait();
                #endregion
                mainLayout.AddView(linearLayout);

            }


        }
        public int DpToPixel(Context context, float dp)
        {
            return (int)(dp * context.Resources.DisplayMetrics.Density);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            string productID = ((Button)sender).Tag.ToString();
            SQLiteConnection connection = DataBase.CheckConnection();
            var products = connection.Table<Product>();
            Product foundProduct = products.Where((x) => x.ProductID == productID).First();
            Toast.MakeText(this, foundProduct.ProductName + " : " + foundProduct.ProductCategory + " : " + foundProduct.ProductType, ToastLength.Long).Show();
        }


        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public void SetSelectedCategory(string category, string type)
        {
            var products = connection.Table<Product>().Where(x => x.ProductCategory == category && x.ProductType == type);
            CreateItems(products);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_home)
            {
                var products = connection.Table<Product>();
                CreateItems(products);

            }
            else if (id == Resource.Id.nav_womenatlethic)
            {
                SetSelectedCategory(ProductCategory.Women.ToString(), ProductType.Sport.ToString());
            }

            else if (id == Resource.Id.nav_womencasual)
            {
                SetSelectedCategory(ProductCategory.Women.ToString(), ProductType.Casual.ToString());
            }
            else if (id == Resource.Id.nav_womenwinterboots)
            {
                SetSelectedCategory(ProductCategory.Women.ToString(), ProductType.Winter.ToString());
            }
            else if (id == Resource.Id.nav_menatlethic)
            {
                SetSelectedCategory(ProductCategory.Man.ToString(), ProductType.Sport.ToString());
            }
            else if (id == Resource.Id.nav_mencasual)
            {
                SetSelectedCategory(ProductCategory.Man.ToString(), ProductType.Casual.ToString());
            }
            else if (id == Resource.Id.nav_menwinterboots)
            {
                SetSelectedCategory(ProductCategory.Man.ToString(), ProductType.Winter.ToString());
            }
            else if (id == Resource.Id.nav_kidsatlethic)
            {
                SetSelectedCategory(ProductCategory.Kids.ToString(), ProductType.Sport.ToString());
            }
            else if (id == Resource.Id.nav_kidscasual)
            {
                SetSelectedCategory(ProductCategory.Kids.ToString(), ProductType.Casual.ToString());
            }
            else if (id == Resource.Id.nav_kidswinterboots)
            {
                SetSelectedCategory(ProductCategory.Kids.ToString(), ProductType.Winter.ToString());
            }
            else if (id == Resource.Id.nav_account)
            {
                //sign in if user didnt sign in already
                if (CustomerInfo.UserConnected == false)
                {
                    //ask to user if he/she wants to sign in
                    Snackbar.Make(FindViewById<DrawerLayout>(Resource.Id.drawer_layout), "You didnt Sign in! Would you like to sign in?", Snackbar.LengthLong)
                .SetAction("YES", (v) => { StartActivity(typeof(SignInActivity)); }).Show();

                }
                else
                {
                    //go to account page
                    Toast.MakeText(this, CustomerInfo.customerAccount.UserName + " has logged in!", ToastLength.Short);
                    //LinearLayout accountLayout = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.account, null);
                    StartActivity(typeof(AccountActivity));
                }
            }
            else if (id == Resource.Id.nav_manage)
            {

            }


            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
    }
}

