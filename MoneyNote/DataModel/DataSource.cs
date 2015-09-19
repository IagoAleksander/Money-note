using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Collections;

namespace MoneyNote.DataModel
{
    public class Notes : IComparable
    {
        public DateTime date { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public int CompareTo(object obj)
        {
            Notes note = obj as Notes;
            if (note == null)
            {
                throw new ArgumentException("error");
            }
            return this.date.CompareTo(note.date);
        }

    }

    public class DataSource
    {
        private ObservableCollection<Notes> _notes;

        const string filename = "notes.json";

        public DataSource()
        {
            _notes = new ObservableCollection<Notes>();
        }

        public async Task<ObservableCollection<Notes>> get_Notes()
        {
            await ensureDataLoaded();
            return _notes;
        }

        private async Task ensureDataLoaded()
        {
            if (_notes.Count == 0)
                await get_NotesDataAsync();

            return;
        }

        private async Task get_NotesDataAsync()
        {
            if (_notes.Count != 0)
                return;

            var JsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Notes>));

            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename))
                {
                    _notes = (ObservableCollection<Notes>)JsonSerializer.ReadObject(stream);
                }
            }
            catch
            {
                _notes = new ObservableCollection<Notes>();
            }
        }

        public async void AddNote (Notes note)
        {
            _notes.Add(note);
            await saveNotesDataAsync();
        }

        public async void DeleteNote(Notes note)
        {
            _notes.Remove(note);
            await saveNotesDataAsync();
        }

        public async void EditNote(Notes note)
        {
            var item = _notes.FirstOrDefault(i => i.date == note.date);
            
            if (item != null)
            {
                item.date = note.date;
                item.Description = note.Description;
                item.Value = note.Value;
            }

            await saveNotesDataAsync();
        }

        public async Task saveNotesDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Notes>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(filename, CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _notes);
            }
        }
       
    }
}
