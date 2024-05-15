using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class NoteService {
    private readonly ZupContext _context;

    public NoteService(ZupContext context) {
        _context = context;
    }

    public Note Create(Note new_note) {
        try {
            _context.Note.Add(new_note);
            _context.SaveChanges();

            return new_note;
        }
        catch {
            throw;
        }
    }

    public List<Note> GetByDepartment(long department_id)
    {
        try {
            List<Note> notes = (from note in _context.Note
                                where note.DepartmentId == department_id
                                select note).ToList();
            return notes;
        }
        catch {
            throw;
        }
    }

    public Note GetNote(long note_id) {
        try {
            Note note = (from n in _context.Note
                            where n.NoteId == note_id
                            select n).FirstOrDefault() ?? throw new DataNotFoundException("Selected note does not exist");
            return note;
        }
        catch {
            throw;
        }
    }
}