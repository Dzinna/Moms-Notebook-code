using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Note
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesViewPage : ContentPage
    {        
        NotesViewModel NotesViewModel { get; }

        public NotesViewPage()
        {
            InitializeComponent();

            NotesViewModel = new NotesViewModel();

            BindingContext = NotesViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public NotesViewPage(NotesViewModel notesViewModel)
        {
            InitializeComponent();

            NotesViewModel = notesViewModel;

            BindingContext = NotesViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            NoteType.Text = NotesViewModel.Note.NoteText;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            

            if(NoteType.Text == null)
            {
                await DisplayAlert("Užrašai", "Neužrašėte jokio užrašo.", "Pildyti");
                return;
            }

            NotesViewModel.Note.NoteText = NoteType.Text;
            NotesViewModel.Note.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateNotes", NotesViewModel.Note);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteNote = await DisplayAlert(
                "Ištrinti užrašą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteNote)
                MessagingCenter.Send(this, "DeleteNote", NotesViewModel.Note);

            await Navigation.PopAsync();
        }
    }
}