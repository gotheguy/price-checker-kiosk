using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
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

namespace PriceChecker
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar.FullScreen", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class BranchesListActivity : AppCompatActivity
    {
        private static readonly string TAG = "BranchesListActivity";
        private ListView lv;
        private ArticulosController controller = new ArticulosController();
        private List<string> CodSucursales = new List<string>();
        private List<string> NombreSucursales = new List<string>();
        private List<Sucursal> Sucursales = new List<Sucursal>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            OverridePendingTransition(Resource.Animation.fade_in, Resource.Animation.fade_out);
            SetContentView(Resource.Layout.activity_branches_list);

            Sucursales = controller.GetSucursales();
            foreach(Sucursal s in Sucursales)
            {
                CodSucursales.Add(s.CodSucursal);
                NombreSucursales.Add(s.NombreSucursal);
            }
            var arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, NombreSucursales);

            lv = (ListView)FindViewById(Resource.Id.listview);
            lv.Adapter = arrayAdapter;
            lv.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(OnListItemClick);
        }

        protected void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                string selectionID = CodSucursales[e.Position];
                Sucursal sucursal = new Sucursal();

                foreach (Sucursal s in Sucursales)
                {
                    if (s.CodSucursal == selectionID)
                    {
                        sucursal = s;
                    }
                }
                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("CodSucursal", sucursal.CodSucursal);
                intent.PutExtra("NombreSucursal", sucursal.NombreSucursal);

                SaveSucursal(sucursal);
                StartActivity(intent);
            }
            catch (SQLiteException ex)
            {
                Log.Error(TAG, "Database related error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, "Error: " + ex.Message);
            }
        }

        public void SaveSucursal(Sucursal sucursal)
        {
            try
            {
                SucursalDatabase sucursalDatabase = new SucursalDatabase();
                sucursalDatabase.DeleteAllSucursalesAsync();
                sucursalDatabase.SaveSucursalAsync(sucursal);
            }
            catch (SQLiteException ex)
            {
                Log.Error(TAG, "Database related error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(TAG, "Error: " + ex.Message);
            }
        }
    }   
}