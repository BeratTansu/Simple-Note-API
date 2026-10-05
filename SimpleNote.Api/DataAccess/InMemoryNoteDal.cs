using SimpleNote.Api.Models;

namespace SimpleNote.Api.DataAccess
{
    public class InMemoryNoteDal
    {
        private List<Note> _notes = new List<Note>()
        {
            new Note{Id = 1, Title = "To do list", Content = "do workout", Category = "Daily", CreationDate = DateTime.UtcNow},
            new Note{Id = 2, Title = "Homeworks", Content = "do api", Category = "Weekly", CreationDate = new DateTime(2023, 10, 25)},
            new Note{Id = 3, Title = "Dont do", Content = "eat dirty", CreationDate = DateTime.UtcNow},
        };

        public List<Note> GetAll()
        {
            return _notes;
        }
    }
}
