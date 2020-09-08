using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PriceChecker.Model;
using SQLite;

namespace PriceChecker.SQLite.Data
{
    public class SucursalDatabase
    {
        private readonly string folderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Sucursales");
        readonly SQLiteAsyncConnection _database;

        public SucursalDatabase()
        {
            System.IO.Directory.CreateDirectory(folderPath);
            string databaseFileName = System.IO.Path.Combine(folderPath, "Branch.db");
            _database = new SQLiteAsyncConnection(databaseFileName);
            _database.CreateTableAsync<Sucursal>().Wait();
        }

        public Task<List<Sucursal>> GetSucursalAsync()
        {
            return _database.Table<Sucursal>().ToListAsync();
        }

        public Task<Sucursal> GetSucursalAsync(string id)
        {
            return _database.Table<Sucursal>()
                            .Where(i => i.CodSucursal == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSucursalAsync(Sucursal sucursal)
        {
            if (GetSucursalAsync(sucursal.CodSucursal).Result != null)
            {
                return _database.UpdateAsync(sucursal);
            }
            else
            {
                return _database.InsertAsync(sucursal);
            }
        }

        public Task<int> DeleteSucursalAsync(Sucursal sucursal)
        {
            return _database.DeleteAsync(sucursal);
        }

        public Task<int> DeleteAllSucursalesAsync()
        {
            return _database.DeleteAllAsync<Sucursal>();
        }
    }
}