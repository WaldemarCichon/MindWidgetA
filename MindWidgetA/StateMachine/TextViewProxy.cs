using System;
using System.Collections.Generic;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using MindWidgetA.StateMachine.RemoteComponents;

namespace MindWidgetA.StateMachine
{
    public class TextViewProxy
    {
        private TextView textView;
        private string _text;
        private ViewStates _visibility;
        private RemoteTextView remoteTextView;

        public TextViewProxy()
        {
            
        }

        public void Register (TextView textView)
        {
            this.textView = textView;
        }

        public void Register (RemoteTextView remoteTextView)
        {
            this.remoteTextView = remoteTextView;
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
                if (textView != null)
                {
                    textView.Text = value;
                }
                if (remoteTextView != null)
                {
                    remoteTextView.Text = value;
                }
            }
        }

        public ViewStates Visibility
        {
            get
            {
                return _visibility;
            }

            set
            {
                _visibility = value;
                if (textView != null)
                {
                    textView.Visibility = _visibility;
                }
                if (remoteTextView != null)
                {
                    remoteTextView.Visiblity = _visibility;
                }

            }
        }
    }
}
