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
using SQLite;

namespace PriceChecker.Model
{
    [Table("Sucursales")]
    public class Sucursal
    {
        [PrimaryKey]
        public string CodSucursal { get; set; }
        public string NombreSucursal { get; set; }

        public override string ToString()
        {
            return "CodSucursal: " + CodSucursal + ", NombreSucursal: " + NombreSucursal;
        }
    }
}