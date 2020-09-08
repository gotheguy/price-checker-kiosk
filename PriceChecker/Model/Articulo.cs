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
    public class Articulo
    {
        public string CodArticulo { get; set; }
        public string Codigobarras { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Moneda { get; set; }
        public string Sucursal { get; set; }
        public string Marca { get; set; }
        public int Cantidad { get; set; }
    }
}