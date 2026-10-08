using Microsoft.AspNetCore.Mvc;
using SimpleNote.Api.DataAccess;
using SimpleNote.Api.Models;

namespace SimpleNote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotesController : ControllerBase
    {
        public NotesController(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        private readonly INoteDal _noteDal;

        [HttpGet]
        public IActionResult GetAll(string? title, string? category)
        {
            List<Note> notes = _noteDal.Search(title, category);
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Add(Note note)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                return BadRequest("Title cannot be empty.");
            }

            Note newNote = _noteDal.Add(note);
            return Created($"api/notes/{newNote.Id}", newNote);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Note? foundNote = _noteDal.GetById(id);

            if (foundNote == null)
            {
                return NotFound();
            }
            
             return Ok(foundNote);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Note note)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                return BadRequest("Title cannot be empty.");
            }

            bool isUpdated = _noteDal.Update(id, note);
            
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _noteDal.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
