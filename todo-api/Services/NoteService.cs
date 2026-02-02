using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using todo_api.Models;
using todo_api.Models.Dtos;

namespace todo_api.Services
{
    public class NoteService(TodoContext context, IConfiguration configuration) : INoteService
    {
        public async Task<NoteDTO> CreateNoteAsync(NoteDTO createdNote)
        {
            Note note = new Note()
            {
                Id = createdNote.Id,
                UserId = createdNote.UserId,
                Title = createdNote.Title,
                Color = createdNote.Color,
                HtmlContent = createdNote.HtmlContent,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };



            context.Notes.Add(note);
            try
            {
                await context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {

                throw new Exception("An error occurred while saving the note to the database.", ex);
            }

            NoteDTO newNote = new NoteDTO()
            {
                Id = note.Id,
                UserId = note.UserId,
                Title = note.Title,
                Color = note.Color,
                HtmlContent = note.HtmlContent
            };

            return newNote;
        }

        public async Task<bool> DeleteNoteAsync(long id)
        {
            var note = await context.Notes.FindAsync(id);
            if (note == null)
            {
                throw new Exception("Note not found.");
            }

            context.Notes.Remove(note);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                throw new Exception("An error occurred while deleting the note from the database.", ex);
            }

            return true;
        }

        public async Task<IEnumerable<NoteDTO>> GetNotesAsync(int userId)
        {
            var result = await context.Notes
            .Where(note => note.UserId == userId)
            .OrderBy(note => note.CreatedAt)
            .Select(note => new NoteDTO()
            {
                Id = note.Id,
                UserId = note.UserId,
                Title = note.Title,
                Color = note.Color,
                HtmlContent = note.HtmlContent,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt

            }).ToListAsync();

            if (result == null)
            {
                throw new Exception("No notes found for the specified user.");
            }

            return result;
        }

        public async Task<NoteDTO> UpdateNoteAsync(long id, NoteDTO updatedNote)
        {
            var note = await context.Notes.FindAsync(id);

            if (note == null)
            {
                throw new Exception("Note not found.");
            }


            note.Id = updatedNote.Id;
            note.UserId = updatedNote.UserId;
            note.Title = updatedNote.Title;
            note.Color = updatedNote.Color;
            note.HtmlContent = updatedNote.HtmlContent;

            note.UpdatedAt = DateTime.UtcNow;

            try
            {

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("An error occurred while updating the note in the database.");
            }

            NoteDTO newNote = new NoteDTO()
            {
                Id = note.Id,
                UserId = note.UserId,
                Title = note.Title,
                Color = note.Color,
                HtmlContent = note.HtmlContent,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt
            };

            return newNote;
        }
    }
}
