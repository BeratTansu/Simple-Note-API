using SimpleNote.Api.Models;

namespace SimpleNote.Api.DataAccess
{
    public class InMemoryNoteDal
    {
        private static List<Note> _notes = new List<Note>()
        {
            new Note{Id = 1, Title = "To do list", Content = "do workout", Category = "Daily", CreationDate = DateTime.UtcNow},
            new Note{Id = 2, Title = "Homeworks", Content = "do api", Category = "Weekly", CreationDate = new DateTime(2023, 10, 25)},
            new Note{Id = 3, Title = "Dont do", Content = "eat dirty", CreationDate = DateTime.UtcNow},
        };

        public List<Note> GetAll()
        {
            return _notes;
        }

        public Note Add(Note note)
        {
            if (_notes.Count == 0)
            {
                note.Id = 1;
            }
            else
            {
                note.Id = _notes.Max(n => n.Id) + 1;
            }

            note.CreationDate = DateTime.UtcNow;
            _notes.Add(note);
            return note;
        }

        public Note? GetById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }

        public bool Update(int id, Note updatedNote)
        {
            Note? foundNote = GetById(id);
            if (foundNote == null)
            {
                return false;
            }
            foundNote.Title = updatedNote.Title;
            foundNote.Content = updatedNote.Content;
            foundNote.Category = updatedNote.Category;
            return true;
        }

        public bool Delete(int id)
        {
            Note? foundNote = GetById(id);
            if (foundNote == null)
            {
                return false;
            }
            _notes.Remove(foundNote);
            return true;
        }

        public List<Note> Search(string? title, string? category)
        {
            IEnumerable<Note> filteredNotes = _notes;
            
            if (!string.IsNullOrWhiteSpace(title))
            {
                filteredNotes = filteredNotes.Where(n => n.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                filteredNotes = filteredNotes.Where(n => n.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return filteredNotes.ToList();
        }
    }
}
