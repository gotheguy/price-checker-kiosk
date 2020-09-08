using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using PriceChecker.Api.Controller;
using PriceChecker.Model;
using PriceChecker.SQLite.Data;
using SQLite;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using Xamarin.Essentials;
using static Android.Views.View;

namespace PriceChecker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar.FullScreen", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape, 
        ResizeableActivity = false, LockTaskMode = "always")]
    public class MainActivity : Activity, EMDKManager.IEMDKListener
    {
        private static readonly string TAG = "MainActivity";
        private readonly ArticulosController controller = new ArticulosController();
        private SucursalDatabase sucursalDatabase = new SucursalDatabase();
        private Sucursal sucursal = null;

        EMDKManager emdkManager = null;
        BarcodeManager barcodeManager = null;
        Scanner scanner = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);
            SetContentView(Resource.Layout.activity_main);
            SetSucursal();

            EMDKResults results = EMDKManager.GetEMDKManager(Android.App.Application.Context, this);
            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                Log.Error(TAG, "Status: EMDKManager object creation failed ...");
            }
            else
            {
                Log.Error(TAG, "Status: EMDKManager object creation succeeded ...");
            }
        }

        public void SetSucursal()
        {
            ImageView imageView = (ImageView)FindViewById(Resource.Id.settings_button);
            List<Sucursal> list = sucursalDatabase.GetSucursalAsync().Result;

            if (list.Count == 0)
            {
                imageView.Click += (e, o) => {
                    Intent intent = new Intent(this, typeof(BranchesListActivity));
                    StartActivity(intent);
                };
                Intent i = Intent;
                sucursal = new Sucursal()
                {
                    CodSucursal = i.GetStringExtra("CodSucursal"),
                    NombreSucursal = i.GetStringExtra("NombreSucursal")
                };
            }
            else
            {
                imageView.Visibility = ViewStates.Invisible;
                sucursal = list[0];
            }
        }

        public override void OnBackPressed(){ }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            InitScanner();
        }

        protected override void OnPause()
        {
            base.OnPause();
            DeinitScanner();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (emdkManager != null)
            {
                emdkManager.Release();
                emdkManager = null;
            }
        }

        void EMDKManager.IEMDKListener.OnOpened(EMDKManager emdkManager)
        {
            this.emdkManager = emdkManager;
            InitScanner();
        }

        void EMDKManager.IEMDKListener.OnClosed()
        {
            if (emdkManager != null)
            {
                emdkManager.Release();
                emdkManager = null;
            }
        }

        public virtual void InitScanner()
        {
            if (emdkManager != null)
            {
                if (barcodeManager == null)
                {
                    try
                    {
                        barcodeManager = (BarcodeManager)emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
                        scanner = barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                        if (scanner != null)
                        {
                            scanner.Data += ScannerData;
                            scanner.Status += ScannerStatus;
                            scanner.Enable();

                            ScannerConfig config = scanner.GetConfig();
                            config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                            config.ScanParams.DecodeLEDFeedback = true;
                            config.DecoderParams.Code39.Enabled = true;
                            config.DecoderParams.Code128.Enabled = false;
                            scanner.SetConfig(config);
                        }
                        else
                        {
                            Log.Error(TAG, "Failed to enable scanner.\n");
                        }
                    }
                    catch (ScannerException e)
                    {
                        Log.Error(TAG, "Error: " + e.Message);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(TAG, "Error: " + ex.Message);
                    }
                }
            }
        }

        public virtual void DeinitScanner()
        {
            if (emdkManager != null)
            {
                if (scanner != null)
                {
                    try
                    {
                        scanner.Data -= ScannerData;
                        scanner.Status -= ScannerStatus;
                        scanner.Disable();
                    }
                    catch (ScannerException e)
                    {
                        Log.Debug(TAG, "Exception:" + e.Result.Description);
                    }
                }

                if (barcodeManager != null)
                {
                    emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }
                barcodeManager = null;
                scanner = null;
            }
        }

        public void ScannerData(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();   

                foreach (ScanDataCollection.ScanData data in scanData)
                {
                    try
                    {
                        var current = Connectivity.NetworkAccess;
                        if (current == Xamarin.Essentials.NetworkAccess.Internet)
                        {
                            Articulo a = controller.GetDatosArticulo(data.Data, sucursal.CodSucursal);
                            Intent intent = new Intent(this, typeof(DetailsActivity));
                            intent.PutExtra("ArticuloCodigo", a.CodArticulo);
                            intent.PutExtra("ArticuloNombre", a.Descripcion);
                            intent.PutExtra("ArticuloMoneda", a.Moneda);
                            intent.PutExtra("ArticuloPrecio", a.Precio);
                            StartActivity(intent);
                        }
                        else
                        {
                            Log.Debug(TAG, "Sin conexion: " + Connectivity.NetworkAccess);
                            throw new WebException();
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        this.RunOnUiThread(() =>
                        {
                            CustomDialog customDialog = new CustomDialog(this, "No pudimos encontrar el producto que estás buscando.");
                            customDialog.Show();
                            Log.Error(TAG, ex.ToString());
                        });
                    }
                    catch (WebException ex)
                    {
                        this.RunOnUiThread(() =>
                        {
                            CustomDialog customDialog = new CustomDialog(this, "Sin conexión a Internet. Vuelve a intentarlo en unos momentos.");
                            customDialog.Show();
                            Log.Error(TAG, ex.ToString());
                        });
                    }
                    catch (Exception ex)
                    {
                        this.RunOnUiThread(() =>
                        {
                            CustomDialog customDialog = new CustomDialog(this, "Ha ocurrido un error al consultar el producto.");
                            customDialog.Show();
                            Log.Error(TAG, ex.ToString());
                        });
                    }
                }
            }
        }

        public void ScannerStatus(object sender, Scanner.StatusEventArgs e)
        {
            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                try
                {
                    if (scanner.IsEnabled && !scanner.IsReadPending)
                    {
                        scanner.Read();
                    }
                }
                catch (ScannerException e1)
                {
                    Log.Error(TAG, e1.Message);
                }
            }
            if (state == StatusData.ScannerStates.Waiting)
            {
                Log.Error(TAG, "Waiting for Trigger Press to scan");
            }
            if (state == StatusData.ScannerStates.Scanning)
            {
                Log.Error(TAG, "Scanning in progress...");
            }
            if (state == StatusData.ScannerStates.Disabled)
            {
                Log.Error(TAG, "Scanner disabled");
            }
            if (state == StatusData.ScannerStates.Error)
            {
                Log.Error(TAG, "Error occurred during scanning");
            }
        }
    }
}

