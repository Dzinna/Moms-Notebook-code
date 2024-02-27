using MomsNotebook.Models.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MomsNotebook.Services.Repositories
{
    public interface ISqlLiteDatabase
    {
        SQLiteAsyncConnection Connection { get; }
        Command CreateTables { get; }
        Task Insert(object @object);
        Task Update(object @object);
        Task Delete(object @object);
        Task<List<Contacts>> ReadAllContacts();
        Task<List<Drugs>> ReadAllDrugs();
        Task<List<Feeding>> ReadAllFeedings();
        Task<List<Heights>> ReadAllBabyHeights();
        Task<List<Weights>> ReadAllBabyWeights();
        Task<List<Bathings>> ReadAllBathings();
        Task<List<Flus>> ReadAllFlus();
        Task<List<Drinks>> ReadAllDrinks();
        Task<List<Dipers>> ReadAllDiperings();
        Task<List<Health>> ReadAllHealth();
        Task<List<Moves>> ReadAllMoves();
        Task<List<Notes>> ReadAllNotes();
        Task<List<Sleep>> ReadAllSleep();
        Task<List<Temperature>> ReadAllTemperature();
        Task<List<Info>> ReadAllInfo();
        Task<List<Tooths>> ReadAllTooths();
        Task<List<Markers>> ReadAllMarkers();
    }
}
