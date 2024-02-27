using EnumsNET;
using MomsNotebook.Models.Database;
using MomsNotebook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Tooth;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class ToothsViewModel : BaseViewModel
    {
        public ObservableCollection<Tooths> ToothsCollection { get; set; } = new ObservableCollection<Tooths>();
        public Tooths Tooth { get; set; } = new Tooths();
        public List<KeyValuePair<int, string>> JawList => GenerateJawList();
        public List<KeyValuePair<int, string>> JawSideList => GenerateJawSideList();
        public List<KeyValuePair<int, string>> ToothsList => GenerateToothsList();

        public Command InitializeCollection { get; }

        private KeyValuePair<int, string> _jawSelectedItem;
        public KeyValuePair<int, string> JawSelectedItem
        {
            get => _jawSelectedItem;
            set => _jawSelectedItem = value;
        }

        private KeyValuePair<int, string> _jawSideSelectedItem;
        public KeyValuePair<int, string> JawSideSelectedItem
        {
            get => _jawSideSelectedItem;
            set => _jawSideSelectedItem = value;
        }

        private KeyValuePair<int, string> _toothSelectedItem;
        public KeyValuePair<int, string> ToothSelectedItem
        {
            get => _toothSelectedItem;
            set => _toothSelectedItem = value;
        }

        public ToothsViewModel()
        {
            InitializeSubscribes();
            Title = "Dantys";
            InitializeCollection = new Command(async () => await Initialize());
        }

        async Task Initialize()
        {
            ToothsCollection.Clear();

            var tooths = await Database.ReadAllTooths();

            foreach (var tooth in tooths)
            {
                ToothsCollection.Add(tooth);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<ToothsPage, Tooths>(this, "AddOrUpdateTooth", async (sender, tooth) =>
            {
                if (tooth.Key == null)
                {
                    tooth.Key = Guid.NewGuid().ToString();
                    await Database.Insert(tooth);
                }
                else
                {
                    await Database.Update(tooth);
                }
            });

            MessagingCenter.Subscribe<ToothsPage, Tooths>(this, "DeleteTooth", async (sender, tooth) =>
            {
                await Database.Delete(tooth);
            });
        }

        List<KeyValuePair<int, string>> GenerateJawList()
        {
            var data = new List<KeyValuePair<int, string>>();
            var enumMembers = Enums
                .GetMembers(typeof(Jaw));

            foreach (var enumMember in enumMembers)
            {
                data.Add(new KeyValuePair<int, string>((int)enumMember.Value, enumMember.Attributes.Get<DescriptionAttribute>().Description));
            }

            return data;
        }

        List<KeyValuePair<int, string>> GenerateJawSideList()
        {
            var data = new List<KeyValuePair<int, string>>();

            var enumMembers = Enums
                .GetMembers(typeof(JawSide));

            foreach (var enumMember in enumMembers)
            {
                data.Add(new KeyValuePair<int, string>((int)enumMember.Value, enumMember.Attributes.Get<DescriptionAttribute>().Description));
            }

            return data;
        }

        List<KeyValuePair<int, string>> GenerateToothsList()
        {
            var data = new List<KeyValuePair<int, string>>();

            var enumMembers = Enums
                .GetMembers(typeof(ToothNumber));

            foreach (var enumMember in enumMembers)
            {
                data.Add(new KeyValuePair<int, string>((int)enumMember.Value, enumMember.Attributes.Get<DescriptionAttribute>().Description));
            }

            return data;
        }
    }
}
