using SimpleNote.Api.Models;

namespace SimpleNote.Api.DataAccess
{
    public interface INoteDal
    {
        List<Note> GetAll();
        Note Add(Note note);
        Note? GetById(int id);
        bool Update(int id, Note note);
        bool Delete(int id);
        List<Note> Search(string? title, string? category);

    }
}
