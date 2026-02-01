using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetNotesAsync(int userId);
        Task<Note> CreateNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(long id, NoteDTO note);
        Task<bool> DeleteNoteAsync(long id);
    }
}
