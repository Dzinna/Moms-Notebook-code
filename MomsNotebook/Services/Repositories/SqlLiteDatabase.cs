using MomsNotebook.Models.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MomsNotebook.Services.Repositories
{
    public class SqlLiteDatabase : ISqlLiteDatabase
    {
        public static string DatabaseName => "Moms_Database.db3";
        public SQLiteAsyncConnection Connection { get; }
        public Command CreateTables { get; }

        private object Lock = new object();

        public SqlLiteDatabase()
        {
            var path = Path.Combine(Environment.
              GetFolderPath(Environment.
              SpecialFolder.Personal), DatabaseName);

            Connection = new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true);

            CreateTables = new Command(() => CreateAllTables());
        }

        public SqlLiteDatabase(string syncDatbaseName)
        {
            var path = Path.Combine(Environment.
              GetFolderPath(Environment.
              SpecialFolder.Personal), syncDatbaseName);

            Connection = new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true);

            CreateTables = new Command(() => CreateAllTables());
        }

        public async void CreateAllTables()
        {
            await Connection.CreateTableAsync<Contacts>();
            await Connection.CreateTableAsync<Drugs>();
            await Connection.CreateTableAsync<Feeding>();
            await Connection.CreateTableAsync<Heights>();
            await Connection.CreateTableAsync<Weights>();
            await Connection.CreateTableAsync<Bathings>();
            await Connection.CreateTableAsync<Dipers>();
            await Connection.CreateTableAsync<Drinks>();
            await Connection.CreateTableAsync<Flus>();
            await Connection.CreateTableAsync<Moves>();
            await Connection.CreateTableAsync<Notes>();
            await Connection.CreateTableAsync<Health>();
            await Connection.CreateTableAsync<Sleep>();
            await Connection.CreateTableAsync<Temperature>();
            await Connection.CreateTableAsync<Info>();
            await Connection.CreateTableAsync<Tooths>();
            await Connection.CreateTableAsync<Markers>();
        }

        public async Task Insert(object @object)
        {
            lock (Lock)
            {
                Connection.InsertAsync(@object);
            }

            await Task.CompletedTask;
        }

        public async Task Update(object @object)
        {
            lock (Lock)
            {
                Connection.UpdateAsync(@object);
            }

            await Task.CompletedTask;
        }

        public async Task Delete(object @object)
        {
            lock (Lock)
            {
                Connection.DeleteAsync(@object);
            }

            await Task.CompletedTask;
        }

        #region Contacts
        public async Task<List<Contacts>> ReadAllContacts()
        {
            return await Connection
                .Table<Contacts>()
                .OrderBy(x => x.ContactName)
                .ToListAsync();
        }
        #endregion

        #region Drugs
        public async Task<List<Drugs>> ReadAllDrugs()
        {
            return await Connection
                .Table<Drugs>()
                .ToListAsync();
        }
        #endregion

        #region Feeding
        public async Task<List<Feeding>> ReadAllFeedings()
        {
            return await Connection
                .Table<Feeding>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region BabyHeights
        public async Task<List<Heights>> ReadAllBabyHeights()
        {
            return await Connection
                .Table<Heights>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region BabyWeights
        public async Task<List<Weights>> ReadAllBabyWeights()
        {
            return await Connection
                .Table<Weights>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Bathing
        public async Task<List<Bathings>> ReadAllBathings()
        {
            return await Connection
                .Table<Bathings>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Flus
        public async Task<List<Flus>> ReadAllFlus()
        {
            return await Connection
                .Table<Flus>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Drinks
        public async Task<List<Drinks>> ReadAllDrinks()
        {
            return await Connection
                .Table<Drinks>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Diperings
        public async Task<List<Dipers>> ReadAllDiperings()
        {
            return await Connection
                .Table<Dipers>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Health
        public async Task<List<Health>> ReadAllHealth()
        {
            return await Connection
                .Table<Health>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Moves
        public async Task<List<Moves>> ReadAllMoves()
        {
            return await Connection
                .Table<Moves>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Notes
        public async Task<List<Notes>> ReadAllNotes()
        {
            return await Connection
                .Table<Notes>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Sleep
        public async Task<List<Sleep>> ReadAllSleep()
        {
            return await Connection
                .Table<Sleep>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Temperature
        public async Task<List<Temperature>> ReadAllTemperature()
        {
            return await Connection
                .Table<Temperature>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Info
        public async Task<List<Info>> ReadAllInfo()
        {
            return await Connection
                .Table<Info>()
                .ToListAsync();
        }
        #endregion

        #region Tooths
        public async Task<List<Tooths>> ReadAllTooths()
        {
            return await Connection
                .Table<Tooths>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion

        #region Markers
        public async Task<List<Markers>> ReadAllMarkers()
        {
            return await Connection
                .Table<Markers>()
                .OrderByDescending(x => x.ActualTime)
                .ToListAsync();
        }
        #endregion
    }
}
