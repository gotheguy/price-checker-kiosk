using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using static Android.Bluetooth.BluetoothClass;

namespace PriceChecker.Model
{
    public class IdleTimer : CountDownTimer
    {
        private object obj;

        public IdleTimer(long startTime, long interval, object obj) : base(startTime, interval)
        {
            this.obj = obj;
        }

        public override void OnTick(long millisUntilFinished) {
            long asd = millisUntilFinished / 1000;
            Log.Debug(this.ToString(), asd.ToString());
        }

        public override void OnFinish()
        {
            if(obj.GetType() == typeof(DetailsActivity))
            {
                DetailsActivity details = (DetailsActivity)obj;
                Intent intent = new Intent(details, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.ClearTop);
                details.StartActivity(intent);
            }
            else if (obj.GetType() == typeof(CustomDialog))
            {
                CustomDialog dialog = (CustomDialog)obj;
                dialog.Dismiss();
            }
            this.Cancel();
        }
    }
}