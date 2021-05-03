
using System.Collections.Generic;
using Android.Net;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using MindWidgetA.StateMachine.RemoteComponents;

namespace MindWidgetA.StateMachine
{
    public class ImageViewProxy
    {
        private ImageView imageView;
        private RemoteImageView remoteImageView;
        private Uri uri;
        private ViewStates _visibility;
        private int _resourceId;

        public ImageViewProxy()
        {
        }

        public void Register (ImageView imageView)
        {
            this.imageView = imageView;
        }

        public void Register (RemoteImageView remoteImageView)
        {
            this.remoteImageView = remoteImageView;
        }

        public Uri Uri
        {
            get
            {
                return uri;
            }

            set
            {
                uri = value;
                if (imageView != null)
                {
                    imageView.SetImageURI(uri);
                }

                if (remoteImageView != null)
                {
                    remoteImageView.SetImageURL(Uri);
                }
            }
        }

        public void SetImageResource(int id)
        {
            _resourceId = id;

            if (imageView != null)
            {
                imageView.SetImageResource(id);
            }
            if (remoteImageView != null)
            {
                remoteImageView.SetImageResource(id);
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
                if (imageView != null)
                {
                    imageView.Visibility = _visibility;
                }
                if (remoteImageView != null)
                {
                    remoteImageView.Visiblity = _visibility;
                }
            }
        }
    }
}
