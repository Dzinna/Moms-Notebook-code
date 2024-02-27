using MomsNotebook.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using MomsNotebook.Views.Helpers.Marker;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace MomsNotebook.ViewModels
{
    public class MarkersViewModel : BaseViewModel
    {
        public EventCollection Events { get; set; } = new EventCollection();
        public Markers Marker { get; set; } = new Markers();

        public MarkersViewModel()
        {
            AddCalendarEvents();
            InitializeSubscribes();
        }

        private async void AddCalendarEvents()
        {
            Events.Clear();

            var markersList = await Database.ReadAllMarkers();

            var markersListTimeEvents = markersList.Select(x => x.ActualTime.Date).Distinct();

            foreach (var time in markersListTimeEvents)
            {
                if (Events.ContainsKey(time))
                {
                    var existingCollection = Events.Where(x => x.Key == time.Date).Select(x => x.Value as List<EventData>).First();
                    Events.Remove(time);

                    var arrayOfData = markersList.Where(x => x.ActualTime.Date == time.Date).ToList();

                    var eventListData = new List<EventData>();

                    foreach (var data in arrayOfData)
                    {
                        eventListData.Add(new EventData
                        {
                            Name = $"Įvykis - {data.ActualTime.ToString("HH:mm")}",
                            Description = data.Text,
                            DateTime = data.ActualTime
                        });
                    }

                    Events.Add(time, existingCollection.Union(eventListData).ToList());
                }
                else
                {
                    var eventListData = new List<EventData>();

                    var arrayOfData = markersList.Where(x => x.ActualTime.Date == time.Date).ToList();

                    foreach (var data in arrayOfData)
                    {
                        eventListData.Add(new EventData
                        {
                            Name = $"Įvykis - {data.ActualTime.ToString("HH:mm")}",
                            Description = data.Text,
                            DateTime = data.ActualTime
                        });
                    }

                    Events.Add(time, eventListData);
                }
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<MarkerPage, Markers>(this, "AddMarker", async (sender, marker) =>
            {
                if (marker.Key == null)
                {
                    marker.Key = Guid.NewGuid().ToString();
                    await Database.Insert(marker);
                }
                else
                {
                    await Database.Update(marker);
                }
            });
        }
    }

    public class EventData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}