using SimpleNote.Api.Models;

namespace SimpleNote.Api.DataAccess
{
    public class EfNoteDal : INoteDal
    {
        public EfNoteDal(SimpleNoteDbContext simpleNoteDbContext)
        {
            _context = simpleNoteDbContext;
        }

        private readonly SimpleNoteDbContext _context;

        public List<Note> GetAll()
        {
            return _context.Notes.ToList();
        }

        public Note Add(Note note)
        {
            note.CreationDate = DateTime.UtcNow;
            _context.Notes.Add(note);
            _context.SaveChanges();
            return note;
        }

        public Note? GetById(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public bool Update(int  id, Note note)
        {
            Note? foundNote = GetById(id);
            if (foundNote == null)
            {
                return false;
            }
            foundNote.Title = note.Title;
            foundNote.Content = note.Content;
            foundNote.Category = note.Category;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Note? foundNote = GetById(id);
            if (foundNote == null)
            {
                return false;
            }
            _context.Notes.Remove(foundNote);
            _context.SaveChanges();
            return true;
        }

        public List<Note> Search(string? title, string? category)
        {
            IQueryable<Note> filteredNotes = _context.Notes;

            if (!string.IsNullOrWhiteSpace(title))
            {
                filteredNotes = filteredNotes.Where(n => n.Title.ToLower().Contains(title.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                filteredNotes = filteredNotes.Where(n => n.Category.ToLower().Equals(category.ToLower()));
            }

            return filteredNotes.ToList();
        }
    }
}
