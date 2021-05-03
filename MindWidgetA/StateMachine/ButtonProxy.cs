using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using MindWidgetA.StateMachine.RemoteComponents;

namespace MindWidgetA.StateMachine
{
    public class ButtonProxy
    {
        private Button button;
        private FloatingActionButton fab;
        private ImageView imageView;
        private ViewStates _visibility;
        private RemoteButton remoteButton;
        private StateMachine stateMachine;
        private Events _event;

        public EventHandler Clicked { get; set; }

        public ButtonProxy(StateMachine stateMachine, Events _event)
        {
            this.stateMachine = stateMachine;
            this._event = _event;
            Clicked = InternalClickHandler;
        }

        private void InternalClickHandler(object sender, EventArgs e)
        {
            stateMachine.PushEvent(_event);
        }

        public void Register(Button button)
        {
            this.button = button;
            button.Click += Clicked;
        }

        public void Register(FloatingActionButton button)
        {
            fab = button;
            button.Click += Clicked;
        }

        public void Register(ImageView imageView)
        {
            this.imageView = imageView;
            imageView.Click += Clicked;
        }

        public void Register(RemoteButton button)
        {
            this.remoteButton = button;
            button.Clicked = Clicked;
        }

        public  ViewStates Visibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                _visibility = value;
                if (button != null)
                {
                    button.Visibility = value;
                }
                if (fab != null)
                {
                    fab.Visibility = value;
                }

                if (imageView != null)
                {
                    imageView.Visibility = value;
                }

                if (remoteButton != null)
                {
                    remoteButton.Visiblity = value;
                }
            }
        }
    }
}
