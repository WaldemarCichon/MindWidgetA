using System;
using Android.Appwidget;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace MindWidgetA.StateMachine.RemoteComponents
{
    public class RemoteButton: RemoteComponent
    {
        public RemoteButton(int componentId): base(componentId)
        {
        }

        public EventHandler Clicked { get; set; }
    }
}
