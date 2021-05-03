using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using MindWidgetA.StateMachine;
using MindWidgetA.StateMachine.RemoteComponents;

namespace MindWidgetA.UI
{
    [BroadcastReceiver(Label = "Mind Widget")]
    [IntentFilter(new string[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [MetaData("android.appwidget.provider", Resource = "@xml/appwidgetprovider")]
    public class MainWidget : AppWidgetProvider 
    {
        private const String HAPPY_BTN_CLICKED = "HappyButtonClicked";
        private const String NEUTRAL_BTN_CLICKED = "NeutralButtonClicked";
        private const String SAD_BTN_CLICKED = "SadButtonClicked";
        private const String INFO_BTN_CLICKED = "InfoBtnClicked";
        private const String BACK_BTN_CLICKED = "BackButtonClicked";
        private const String OK_BTN_CLICKED = "OkButtonClicked";
        private const String NO_BTN_CLICKED = "NoButtonClicked";
        
        public MainWidget()
        {

        }

        public override void OnUpdate(Context context, AppWidgetManager appWidgetManager, int[] appWidgetIds)
        {
            RemoteViews remoteViews;
            ComponentName widget;

            remoteViews = new RemoteViews(context.PackageName, Resource.Layout.widget_main);
            widget = new ComponentName(context, Java.Lang.Class.FromType(typeof(MainWidget))); //  new ComponentName(context, typeof(Widget));
                        
            var ui = AbstractUI.Instance;
            ui.SetBaseData(remoteViews, widget, appWidgetManager);
            
            remoteViews.SetOnClickPendingIntent(Resource.Id.info_widget, GetPendingSelfIntent(context, INFO_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.happy_widget, GetPendingSelfIntent(context, HAPPY_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.neutral_widget, GetPendingSelfIntent(context, NEUTRAL_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.sad_widget, GetPendingSelfIntent(context, SAD_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.back_widget, GetPendingSelfIntent(context, BACK_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.ok_widget, GetPendingSelfIntent(context, OK_BTN_CLICKED));
            remoteViews.SetOnClickPendingIntent(Resource.Id.no_widget, GetPendingSelfIntent(context, NO_BTN_CLICKED));

            appWidgetManager.UpdateAppWidget(widget, remoteViews);
            
        }

        public override void OnReceive(Context context, Intent intent) {
            base.OnReceive(context, intent);
            switch (intent.Action)
            {
                case INFO_BTN_CLICKED: AbstractUI.Instance.InfoButton.Clicked(this, null); break;
                case HAPPY_BTN_CLICKED: AbstractUI.Instance.HappyButton.Clicked(this, null); break;
                case NEUTRAL_BTN_CLICKED: AbstractUI.Instance.NeutralButton.Clicked(this, null); break;
                case SAD_BTN_CLICKED: AbstractUI.Instance.SadButton.Clicked(this, null); break;
                case BACK_BTN_CLICKED: AbstractUI.Instance.BackButton.Clicked(this, null); break;
                case OK_BTN_CLICKED: AbstractUI.Instance.OkButton.Clicked(this, null); break;
                case NO_BTN_CLICKED: AbstractUI.Instance.NoButton.Clicked(this, null); break;
            }
            Console.WriteLine("In OnReceive");
            Console.WriteLine(intent.Action, intent.Component);
            RemoteViews remoteViews = new RemoteViews(context.PackageName, Resource.Layout.widget_main);
            ComponentName widget = new ComponentName(context, Java.Lang.Class.FromType(typeof(MainWidget)));
        }

        protected PendingIntent GetPendingSelfIntent(Context context, String action)
        {
            Intent intent = new Intent(context, typeof(MainWidget));
            intent.SetAction(action);
            return PendingIntent.GetBroadcast(context, 0, intent, 0);
        }
    }
}