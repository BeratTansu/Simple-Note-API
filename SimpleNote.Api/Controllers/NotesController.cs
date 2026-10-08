using Microsoft.AspNetCore.Mvc;
using SimpleNote.Api.DataAccess;
using SimpleNote.Api.Models;

namespace SimpleNote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotesController : ControllerBase
    {
        public NotesController(EfNoteDal efNoteDal)
        {
            _efNoteDal = efNoteDal;
        }

        private readonly EfNoteDal _efNoteDal;

        [HttpGet]
        public IActionResult GetAll(string? title, string? category)
        {
            List<Note> notes = _efNoteDal.Search(title, category);
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Add(Note note)
        {
            if (string.IsNullOrWhiteSpace(note.Title))
            {
                return BadRequest("Title cannot be empty.");
            }

            Note newNote = _efNoteDal.Add(note);
            return Created($"api/notes/{newNote.Id}", newNote);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Note? foundNote = _efNoteDal.GetById(id);

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

            bool isUpdated = _efNoteDal.Update(id, note);
            
            if (!isUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _efNoteDal.Delete(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
