using Microsoft.AspNetCore.Mvc;
using SimpleNote.Api.DataAccess;
using SimpleNote.Api.Models;

namespace SimpleNote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotesController : ControllerBase
    {
        private InMemoryNoteDal _inMemoryNoteDal = new InMemoryNoteDal();

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Note> notes = _inMemoryNoteDal.GetAll();
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult Add(Note note)
        {
            Note newNote = _inMemoryNoteDal.Add(note);
            return Created($"api/notes/{newNote.Id}", newNote);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Note? foundNote = _inMemoryNoteDal.GetById(id);

            if (foundNote == null)
            {
                return NotFound();
            }
            
             return Ok(foundNote);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Note note)
        {
            bool isUpdated = _inMemoryNoteDal.Update(id, note);
            
            if (isUpdated == true)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
