using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Note
{
    public partial class NotesPage : ContentPage
    {
        public NotesViewModel NotesViewModel { get; set; }

        public NotesPage()
        {
            InitializeComponent();

            NotesViewModel = new NotesViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new NotesViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            NotesViewModel.NoteCommand.Execute(null);

            NotesListView.ItemsSource = NotesViewModel.NotesCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var note = NotesListView.SelectedItem as Notes;

            if (note == null)
            {
                return;
            }

            NotesViewModel.Note = note;

            await Navigation.PushAsync(new NotesViewPage(NotesViewModel));

            NotesListView.SelectedItem = null;
        }
    }
}