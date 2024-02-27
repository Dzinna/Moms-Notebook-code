using MomsNotebook.Models.Database;
using MomsNotebook.Services.Ftp;
using MomsNotebook.Services.Repositories;
using MomsNotebook.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MomsNotebook.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private ISqlLiteDatabase Sqlite { get; }
        private IFtpSyncService FtpClient { get; }
        private IMySqlDatabase MySql { get; }
        public SynchronizationService()
        {
            Sqlite = DependencyService.Get<ISqlLiteDatabase>();
            FtpClient = DependencyService.Get<IFtpSyncService>();
            MySql = DependencyService.Get<IMySqlDatabase>();
        }
        public async Task Synchronize()
        {
            // Sqlite
            var syncSqlLite = new SqlLiteDatabase(SqlLiteDatabase.DatabaseName.Replace("Database", "Database_New"));

            var babyHeightsArrayNew = await syncSqlLite.ReadAllBabyHeights();
            var babyHeightsArrayOld = await Sqlite.ReadAllBabyHeights();

            foreach (var elementNew in babyHeightsArrayNew)
            {
                foreach (var elementOld in babyHeightsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Height = elementNew.Height;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var babyHeightsArrayAdd = babyHeightsArrayNew.Where(x => !babyHeightsArrayOld.Contains(x, new CompareData<Heights>()));
            var babyHeightsArrayRemove = babyHeightsArrayOld.Where(x => !babyHeightsArrayNew.Contains(x, new CompareData<Heights>()));

            if (babyHeightsArrayAdd.Any())
            {
                foreach (var elem in babyHeightsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (babyHeightsArrayRemove.Any())
            {
                foreach (var elem in babyHeightsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var babyWeightsArrayNew = await syncSqlLite.ReadAllBabyWeights();
            var babyWeightsArrayOld = await Sqlite.ReadAllBabyWeights();

            foreach (var elementNew in babyWeightsArrayNew)
            {
                foreach (var elementOld in babyWeightsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Weight = elementNew.Weight;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var babyWeightsArrayAdd = babyWeightsArrayNew.Where(x => !babyWeightsArrayOld.Contains(x, new CompareData<Weights>()));
            var babyWeightsArrayRemove = babyWeightsArrayOld.Where(x => !babyWeightsArrayNew.Contains(x, new CompareData<Weights>()));

            if (babyWeightsArrayAdd.Any())
            {
                foreach (var elem in babyWeightsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (babyWeightsArrayRemove.Any())
            {
                foreach (var elem in babyWeightsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var bathingsArrayNew = await syncSqlLite.ReadAllBathings();
            var bathingsArrayOld = await Sqlite.ReadAllBathings();

            foreach (var elementNew in bathingsArrayNew)
            {
                foreach (var elementOld in bathingsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Text = elementNew.Text;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var bathingsArrayAdd = bathingsArrayNew.Where(x => !bathingsArrayOld.Contains(x, new CompareData<Bathings>()));
            var bathingsArrayRemove = bathingsArrayOld.Where(x => !bathingsArrayNew.Contains(x, new CompareData<Bathings>()));

            if (bathingsArrayAdd.Any())
            {
                foreach (var elem in bathingsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (bathingsArrayRemove.Any())
            {
                foreach (var elem in bathingsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var contactsArrayNew = await syncSqlLite.ReadAllContacts();
            var contactsArrayOld = await Sqlite.ReadAllContacts();

            foreach (var elementNew in contactsArrayNew)
            {
                foreach (var elementOld in contactsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Address = elementNew.Address;
                        elementOld.ContactName = elementNew.ContactName;
                        elementOld.ContactType = elementNew.ContactType;
                        elementOld.FlatNumber = elementNew.FlatNumber;
                        elementOld.HouseNumber = elementNew.HouseNumber;
                        elementOld.Mailbox = elementNew.Mailbox;
                        elementOld.Telephone = elementNew.Telephone;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var contactsArrayAdd = contactsArrayNew.Where(x => !contactsArrayOld.Contains(x, new CompareData<Contacts>()));
            var contactsArrayRemove = contactsArrayOld.Where(x => !contactsArrayNew.Contains(x, new CompareData<Contacts>()));

            if (contactsArrayAdd.Any())
            {
                foreach (var elem in contactsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (contactsArrayRemove.Any())
            {
                foreach (var elem in contactsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var diperingsArrayNew = await syncSqlLite.ReadAllDiperings();
            var diperingsArrayOld = await Sqlite.ReadAllDiperings();

            foreach (var elementNew in diperingsArrayNew)
            {
                foreach (var elementOld in diperingsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Text = elementNew.Text;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var diperingsArrayAdd = diperingsArrayNew.Where(x => !diperingsArrayOld.Contains(x, new CompareData<Dipers>()));
            var diperingsArrayRemove = diperingsArrayOld.Where(x => !diperingsArrayNew.Contains(x, new CompareData<Dipers>()));

            if (diperingsArrayAdd.Any())
            {
                foreach (var elem in diperingsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (diperingsArrayRemove.Any())
            {
                foreach (var elem in diperingsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var drinksArrayNew = await syncSqlLite.ReadAllDrinks();
            var drinksArrayOld = await Sqlite.ReadAllDrinks();

            foreach (var elementNew in drinksArrayNew)
            {
                foreach (var elementOld in drinksArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.DrinkText = elementNew.DrinkText;
                        elementOld.ActualTime = elementNew.ActualTime;
                        elementOld.Quantity = elementNew.Quantity;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var drinksArrayAdd = drinksArrayNew.Where(x => !drinksArrayOld.Contains(x, new CompareData<Drinks>()));
            var drinksArrayRemove = drinksArrayOld.Where(x => !drinksArrayNew.Contains(x, new CompareData<Drinks>()));

            if (drinksArrayAdd.Any())
            {
                foreach (var elem in drinksArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (drinksArrayRemove.Any())
            {
                foreach (var elem in drinksArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var drugsArrayNew = await syncSqlLite.ReadAllDrugs();
            var drugsArrayOld = await Sqlite.ReadAllDrugs();

            foreach (var elementNew in drugsArrayNew)
            {
                foreach (var elementOld in drugsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.DrugName = elementNew.DrugName;
                        elementOld.ActualTime = elementNew.ActualTime;
                        elementOld.DrugSize = elementNew.DrugSize;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var drugsArrayAdd = drugsArrayNew.Where(x => !drugsArrayOld.Contains(x, new CompareData<Drugs>()));
            var drugsArrayRemove = drugsArrayOld.Where(x => !drugsArrayNew.Contains(x, new CompareData<Drugs>()));

            if (drugsArrayAdd.Any())
            {
                foreach (var elem in drugsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (drugsArrayRemove.Any())
            {
                foreach (var elem in drugsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var feedingsArrayNew = await syncSqlLite.ReadAllFeedings();
            var feedingsArrayOld = await Sqlite.ReadAllFeedings();

            foreach (var elementNew in feedingsArrayNew)
            {
                foreach (var elementOld in feedingsArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.FoodDescription = elementNew.FoodDescription;
                        elementOld.ActualTime = elementNew.ActualTime;
                        elementOld.Quantity = elementNew.Quantity;
                        elementOld.FoodType = elementNew.FoodType;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var feedingsArrayAdd = feedingsArrayNew.Where(x => !feedingsArrayOld.Contains(x, new CompareData<Feeding>()));
            var feedingsArrayRemove = feedingsArrayOld.Where(x => !feedingsArrayNew.Contains(x, new CompareData<Feeding>()));

            if (feedingsArrayAdd.Any())
            {
                foreach (var elem in feedingsArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (feedingsArrayRemove.Any())
            {
                foreach (var elem in feedingsArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var flusArrayNew = await syncSqlLite.ReadAllFlus();
            var flusArrayOld = await Sqlite.ReadAllFlus();

            foreach (var elementNew in flusArrayNew)
            {
                foreach (var elementOld in flusArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.FluName = elementNew.FluName;
                        elementOld.ActualTime = elementNew.ActualTime;
                        elementOld.Notes = elementNew.Notes;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var flusArrayAdd = flusArrayNew.Where(x => !flusArrayOld.Contains(x, new CompareData<Flus>()));
            var flusArrayRemove = flusArrayOld.Where(x => !flusArrayNew.Contains(x, new CompareData<Flus>()));

            if (flusArrayAdd.Any())
            {
                foreach (var elem in flusArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (flusArrayRemove.Any())
            {
                foreach (var elem in flusArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var healthArrayNew = await syncSqlLite.ReadAllHealth();
            var healthArrayOld = await Sqlite.ReadAllHealth();

            foreach (var elementNew in healthArrayNew)
            {
                foreach (var elementOld in healthArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Text = elementNew.Text;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var healthArrayAdd = healthArrayNew.Where(x => !healthArrayOld.Contains(x, new CompareData<Health>()));
            var healthArrayRemove = healthArrayOld.Where(x => !healthArrayNew.Contains(x, new CompareData<Health>()));

            if (healthArrayAdd.Any())
            {
                foreach (var elem in healthArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (healthArrayRemove.Any())
            {
                foreach (var elem in healthArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var movesArrayNew = await syncSqlLite.ReadAllMoves();
            var movesArrayOld = await Sqlite.ReadAllMoves();

            foreach (var elementNew in movesArrayNew)
            {
                foreach (var elementOld in movesArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Text = elementNew.Text;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var movesArrayAdd = movesArrayNew.Where(x => !movesArrayOld.Contains(x, new CompareData<Moves>()));
            var movesArrayRemove = movesArrayOld.Where(x => !movesArrayNew.Contains(x, new CompareData<Moves>()));

            if (movesArrayAdd.Any())
            {
                foreach (var elem in movesArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (movesArrayRemove.Any())
            {
                foreach (var elem in movesArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var notesArrayNew = await syncSqlLite.ReadAllNotes();
            var notesArrayOld = await Sqlite.ReadAllNotes();

            foreach (var elementNew in notesArrayNew)
            {
                foreach (var elementOld in notesArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.NoteText = elementNew.NoteText;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var notesArrayAdd = notesArrayNew.Where(x => !notesArrayOld.Contains(x, new CompareData<Notes>()));
            var notesArrayRemove = notesArrayOld.Where(x => !notesArrayNew.Contains(x, new CompareData<Notes>()));

            if (notesArrayAdd.Any())
            {
                foreach (var elem in notesArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (notesArrayRemove.Any())
            {
                foreach (var elem in notesArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var sleepArrayNew = await syncSqlLite.ReadAllSleep();
            var sleepArrayOld = await Sqlite.ReadAllSleep();

            foreach (var elementNew in sleepArrayNew)
            {
                foreach (var elementOld in sleepArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.EndTime = elementNew.EndTime;
                        elementOld.ActualTime = elementNew.ActualTime;
                        elementOld.SleepText = elementNew.SleepText;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var sleepArrayAdd = sleepArrayNew.Where(x => !sleepArrayOld.Contains(x, new CompareData<Sleep>()));
            var sleepArrayRemove = sleepArrayOld.Where(x => !sleepArrayNew.Contains(x, new CompareData<Sleep>()));

            if (sleepArrayAdd.Any())
            {
                foreach (var elem in sleepArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (sleepArrayRemove.Any())
            {
                foreach (var elem in sleepArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            var temperatureArrayNew = await syncSqlLite.ReadAllTemperature();
            var temperatureArrayOld = await Sqlite.ReadAllTemperature();

            foreach (var elementNew in temperatureArrayNew)
            {
                foreach (var elementOld in temperatureArrayOld)
                {
                    if (elementNew.Key == elementOld.Key)
                    {
                        elementOld.Value = elementNew.Value;
                        elementOld.ActualTime = elementNew.ActualTime;

                        await Sqlite.Update(elementOld);
                    }
                }
            }

            var temperatureArrayAdd = temperatureArrayNew.Where(x => !temperatureArrayOld.Contains(x, new CompareData<Temperature>()));
            var temperatureArrayRemove = temperatureArrayOld.Where(x => !temperatureArrayNew.Contains(x, new CompareData<Temperature>()));

            if (temperatureArrayAdd.Any())
            {
                foreach (var elem in temperatureArrayAdd)
                {
                    await Sqlite.Insert(elem);
                }
            }

            if (temperatureArrayRemove.Any())
            {
                foreach (var elem in temperatureArrayRemove)
                {
                    await Sqlite.Delete(elem);
                }
            }

            await UpdateInfoTable();

            File.Delete(GlobalProperties.DatabasePath.Replace("Database", "Database_New"));

            await FtpClient.UploadFileAsync(GlobalProperties.Uuid);
        }

        public async Task UpdateInfoTable()
        {
            if (MySql.Active && GlobalProperties.DatabaseFileExists && GlobalProperties.UserConnected)
            {
                var infoData = MySql.GetDataFromInfoTable();

                var localInfoData = await Sqlite.ReadAllInfo();

                if (localInfoData.Any())
                {
                    foreach (var info in localInfoData)
                    {
                        await Sqlite.Delete(info);
                    }

                    foreach (var info in infoData)
                    {
                        await Sqlite.Insert(info);
                    }
                }
                else
                {
                    foreach (var info in infoData)
                    {
                        await Sqlite.Insert(info);
                    }
                }


            }
        }
    }

    public class CompareData<T> : IEqualityComparer<T> where T : ITable
    {
        public bool Equals(T x, T y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Key == y.Key;
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
