using Android.Net;

namespace MindWidgetA.StateMachine.RemoteComponents
{
    public class RemoteImageView: RemoteComponent
    {
        public RemoteImageView(int componentId): base(componentId)
        {
        }

        public void SetImageURL(Uri uri)
        {
            RemoteViews.SetImageViewUri(ComponentId, uri);
            AppWidgetManager.UpdateAppWidget(Widget, RemoteViews);
        }

        internal void SetImageResource(int id)
        {
            RemoteViews.SetImageViewResource(ComponentId, id);
            AppWidgetManager.UpdateAppWidget(Widget, RemoteViews);
        }
    }
}
