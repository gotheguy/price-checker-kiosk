using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PriceChecker.Model;

namespace PriceChecker.Api.Controller
{
    public class ArticulosController
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();

        public ArticulosController()
        {
            client.BaseAddress = new Uri(Access.BaseURL);
        }

        public List<Sucursal> GetSucursales()
        {
            try
            {
                List<Sucursal> sucursales = new List<Sucursal>();
                response = client.GetAsync("articulos/getSucursalZona").Result;
                
                if (response.IsSuccessStatusCode)
                {
                    var read = response.Content.ReadAsStringAsync().Result;
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject(read);
                    JArray jArray = JArray.Parse(result.ToString());
                    
                    foreach (JToken a in jArray)
                    {
                        Sucursal sucursal = new Sucursal()
                        {
                            CodSucursal = (string)a.SelectToken("idsucursal"),
                            NombreSucursal = (string)a.SelectToken("sucursal")
                        };
                        sucursales.Add(sucursal);
                    }
                    return sucursales;
                }
            }
            catch (Exception e)
            {
                Log.Error(this.ToString(), "Exception:" + e);
            }
            return null;
        }

        public Articulo GetDatosArticulo(string CodBarra, string CodSucursal)
        {
            try
            {
                response = client.GetAsync("articulos/getSearchArtPriceTest?buscar=" + CodBarra + "&sucursal=" + CodSucursal).Result;

                if (response.IsSuccessStatusCode)
                {
                    var read = response.Content.ReadAsStringAsync().Result;
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject(read);

                    JObject jObject = JObject.Parse(result.ToString());

                    Articulo articulo = new Articulo()
                    {
                        CodArticulo = (string)jObject.SelectToken("idArticulo"),
                        Codigobarras = (string)jObject.SelectToken("codigobarras"),
                        Descripcion = (string)jObject.SelectToken("descripcion"),
                        Moneda = (string)jObject.SelectToken("moneda"),
                        Precio = (double)jObject.SelectToken("precio"),
                        Marca = (string)jObject.SelectToken("marca")
                    };
                    return articulo;
                }
            }
            catch (Exception e)
            {
                Log.Error(this.ToString(), "Exception:" + e);
            }
            return null;
        }
    }
}