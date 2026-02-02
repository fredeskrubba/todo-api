using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDTO>> GetNotesAsync(int userId);
        Task<NoteDTO> CreateNoteAsync(NoteDTO note);
        Task<NoteDTO> UpdateNoteAsync(long id, NoteDTO note);
        Task<bool> DeleteNoteAsync(long id);
    }
}
