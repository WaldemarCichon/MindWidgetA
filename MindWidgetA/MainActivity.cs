using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using MindWidgetA.Texts;
using MindWidgetA.StateMachine;

namespace MindWidgetA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public Android.Widget.TextView mainTextView;
        public Android.Widget.ImageView backgroundImageView;
        public FloatingActionButton happyButton;
        public FloatingActionButton neutralButton;
        public FloatingActionButton sadButton;
        public FloatingActionButton okButton;
        public FloatingActionButton laterButton;
        public FloatingActionButton noButton;
        public Android.Widget.Button infoButton;
        //public Android.Widget.Button backButton;
        public Android.Widget.Button confirmButton;
        public Android.Widget.ImageView backButton;
        
        public MainActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var ui = AbstractUI.Instance;
            happyButton = FindViewById<FloatingActionButton>(Resource.Id.happy);
            ui.HappyButton.Register(happyButton);

            neutralButton = FindViewById<FloatingActionButton>(Resource.Id.neutral);
            ui.NeutralButton.Register(neutralButton);

            sadButton = FindViewById<FloatingActionButton>(Resource.Id.sad);
            ui.SadButton.Register(sadButton);

            infoButton = FindViewById<Android.Widget.Button>(Resource.Id.info);
            ui.InfoButton.Register(infoButton);

            okButton = FindViewById<FloatingActionButton>(Resource.Id.ok);
            ui.OkButton.Register(okButton);

            noButton = FindViewById<FloatingActionButton>(Resource.Id.no);
            ui.NoButton.Register(noButton);

            laterButton = FindViewById<FloatingActionButton>(Resource.Id.later);
            ui.LaterButton.Register(laterButton);

            backButton = FindViewById<Android.Widget.ImageView>(Resource.Id.back);
            ui.BackButton.Register(backButton);

            mainTextView = FindViewById<Android.Widget.TextView>(Resource.Id.mainText);
            ui.MainText.Register(mainTextView);

            backgroundImageView = FindViewById<Android.Widget.ImageView>(Resource.Id.backgroundImage);
            ui.Background.Register(backgroundImageView);

            var include = FindViewById(Resource.Id.main_include);
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
