using System;
using Android.Appwidget;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace MindWidgetA.StateMachine.RemoteComponents
{
    public class RemoteComponent
    {
        private ViewStates _visibility;

        public RemoteComponent(int componentId)
        {
            ComponentId = componentId;
        }

        protected int ComponentId { get; set; }
        public AppWidgetManager AppWidgetManager { get; set; }
        public RemoteViews RemoteViews { get; set; }
        public ComponentName Widget { get; set; }

        public ViewStates Visiblity
        {
            get
            {
                return _visibility;
            }

            set
            {
                _visibility = value;
                RemoteViews.SetViewVisibility(ComponentId, value);
                AppWidgetManager.UpdateAppWidget(Widget, RemoteViews);
            }
        }

        public void SetBaseData(RemoteViews remoteViews, ComponentName widget, AppWidgetManager appWidgetManager)
        {
            RemoteViews = remoteViews;
            Widget = widget;
            AppWidgetManager = appWidgetManager;
        }
    }
}
