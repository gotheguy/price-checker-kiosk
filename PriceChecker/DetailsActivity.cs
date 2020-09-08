using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using PriceChecker.Model;
using Android.Support.V7.App;
using Android.Views;

namespace PriceChecker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar.FullScreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class DetailsActivity : AppCompatActivity
    {
        private TextView ArticuloNombre = null;
        private TextView ArticuloCodigo = null;
        private TextView ArticuloPrecio = null;
        private const int startTime = 3000;
        private const long interval = 1000;
        private IdleTimer timer = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);

            SetContentView(Resource.Layout.activity_details);

            timer = new IdleTimer(startTime, interval, this);
            timer.Start();

            Intent intent = Intent;
            string ArtCodigo = intent.GetStringExtra("ArticuloCodigo");
            string ArtNombre = intent.GetStringExtra("ArticuloNombre");
            string ArtMoneda = intent.GetStringExtra("ArticuloMoneda");
            double ArtPrecio = intent.GetDoubleExtra("ArticuloPrecio", 0);

            ArticuloNombre = FindViewById<TextView>(Resource.Id.articulo_nombre);
            ArticuloNombre.Text = ArtNombre;

            ArticuloCodigo = FindViewById<TextView>(Resource.Id.articulo_codigo);
            ArticuloCodigo.Text = "SKU: " + ArtCodigo;

            ArticuloPrecio = FindViewById<TextView>(Resource.Id.articulo_precio);
            ArticuloPrecio.Text = ArtMoneda + ArtPrecio;
        }
    }
}
