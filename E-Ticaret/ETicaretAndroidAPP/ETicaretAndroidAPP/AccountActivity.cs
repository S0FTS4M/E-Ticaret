using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.refractored;
using Java.Lang;
using static Android.App.ActionBar;

namespace ETicaretAndroidAPP
{
    [Activity(Label = "AccountActivity", Theme = "@style/AppTheme.Ligt.NoActionBar")]
    public class AccountActivity : AppCompatActivity
    {
        MyAdapter myAdapter;
        PagerSlidingTabStrip tabStrip;
        ViewPager pager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.account);

            myAdapter = new MyAdapter(SupportFragmentManager);
            pager = FindViewById<ViewPager>(Resource.Id.pager);
            tabStrip = FindViewById<PagerSlidingTabStrip>(Resource.Id.tabs);
            pager.Adapter = myAdapter;
            tabStrip.SetViewPager(pager);
            tabStrip.SetBackgroundColor(Android.Graphics.Color.Argb(255, 0, 149, 164));
            // Create your application here
        }
        public class MyAdapter : FragmentPagerAdapter
        {
            int tabCount = 2;
            public MyAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
            {
            }

            public override int Count { get { return tabCount; } }
            public override ICharSequence GetPageTitleFormatted(int position)
            {
                ICharSequence cs;
                if (position == 0)
                    cs =new Java.Lang.String("Account");
                else
                if (position == 1)
                    cs = new Java.Lang.String("Personal");
                else
                    cs = new Java.Lang.String("NONE");
                return cs;
            }
            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return ContentFragment.NewInstace(position);
            }
        }
    }
}