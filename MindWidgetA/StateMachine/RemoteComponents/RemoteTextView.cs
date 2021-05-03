using System;
namespace MindWidgetA.StateMachine.RemoteComponents
{
    public class RemoteTextView: RemoteComponent
    {
        private String _text;

        public RemoteTextView(int componentId): base(componentId)
        {
        }

        public String Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                RemoteViews.SetTextViewText(ComponentId, value);
                AppWidgetManager.UpdateAppWidget(Widget, RemoteViews);
            }
        }

    }
}
