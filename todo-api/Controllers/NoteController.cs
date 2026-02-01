using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todo_api.Models;
using todo_api.Services;
using todo_api.Models.Dtos;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController(INoteService noteService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNote(Note createdNote)
        {


            var note = await noteService.CreateNoteAsync(createdNote);

            return Ok(note);

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotes(int userId)
        {
            var result = await noteService.GetNotesAsync(userId);

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(long id, NoteDTO note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await noteService.UpdateNoteAsync(id, note);


            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(long id)
        {
            var result = await noteService.DeleteNoteAsync(id);

            return Ok(result);
        }
    }
}
