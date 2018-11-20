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

namespace ETicaretAndroidAPP
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            
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
            LinearLayout template = gridLayout.FindViewById<LinearLayout>(Resource.Id.basicCart);
             CreateItems(gridLayout,template);
          
          

        }
         void CreateItems(GridLayout gridLayout,LinearLayout template)
        {

            List<LinearLayout> linearList = new List<LinearLayout>(); 
                for (int i = 0; i < 6; i++)
                {
                LinearLayout linearLayout = (LinearLayout)LayoutInflater.Inflate(Resource.Layout.basicCart,null);

                Button button= linearLayout.FindViewById<Button>(Resource.Id.showButton);
                button.Tag = i;
                button.Click += Button_Click;
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
                linearList.Add(linearLayout);
                     //gridLayout.Wait();
                }
            foreach (var item in linearList)
            {
                gridLayout.AddView(item);

            }

        }
        public int DpToPixel(Context context, float dp)
        {
            return (int)(dp * context.Resources.DisplayMetrics.Density);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.textButton);
            textView.Text = ((Button)sender).Tag.ToString();
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

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_home)
            {
                // Handle the camera action
            
            }
            else if (id == Resource.Id.nav_womenatlethic)
            {

            }
            else if (id == Resource.Id.nav_account)
            {
                //sign in if user didnt sign in already
                if (CustomerInfo.UserConnected == false)
                    StartActivity(typeof(SignInActivity));
                else
                    //go to account page
                    Toast.MakeText(this, CustomerInfo.customerAccount.UserName + " has logged in!", ToastLength.Short);
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

