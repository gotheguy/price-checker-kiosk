using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PriceChecker.Model
{
    public class CustomDialog : Dialog
    {
        private TextView dialogText = null;
        private string dialogError = null;
        private const int startTime = 5000;
        private const long interval = 1000;
        private IdleTimer timer = null;
        public CustomDialog(Activity activity, string dialogError) : base(activity)
        {
            this.dialogError = dialogError;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.custom_dialog_alert);

            timer = new IdleTimer(startTime, interval, this);
            timer.Start();

            dialogText = FindViewById<TextView>(Resource.Id.dialog_error);
            dialogText.Text = this.dialogError;

            Button cancel = (Button)FindViewById(Resource.Id.cancel_action);
            cancel.Click += (e, a) =>
            {
                Dismiss();
            };
        }
    }
}