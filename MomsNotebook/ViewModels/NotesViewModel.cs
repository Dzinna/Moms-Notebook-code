using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Note;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        public ObservableCollection<Notes> NotesCollection { get; set; } = new ObservableCollection<Notes>();
        public Notes Note { get; set; } = new Notes();
        public Command NoteCommand { get; set; }

        public NotesViewModel()
        {
            NoteCommand = new Command(() => Initialize());

            InitializeSubscribes();
        }

        async void Initialize()
        {
            NotesCollection.Clear();

            var notesData = await Database.ReadAllNotes();

            foreach (var note in notesData)
            {
                NotesCollection.Add(note);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<NotesViewPage, Notes>(this, "AddOrUpdateNotes", async (sender, note) =>
            {
                if (note.Key == null)
                {
                    note.Key = Guid.NewGuid().ToString();
                    await Database.Insert(note);
                }
                else
                {
                    await Database.Update(note);
                }
            });

            MessagingCenter.Subscribe<NotesViewPage, Notes>(this, "DeleteNote", async (sender, note) =>
            {
                await Database.Delete(note);
            });
        }
    }
}
