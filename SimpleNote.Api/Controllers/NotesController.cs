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

    }
}
