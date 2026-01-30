using todo_api.Models;

namespace todo_api.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetNotesAsync(int userId);
        Task<Note> CreateNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(long id, Note note);
        Task<bool> DeleteNoteAsync(long id);
    }
}
