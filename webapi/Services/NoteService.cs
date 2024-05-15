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

    public List<Note> GetAllNotes(long department_id) {
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

    public bool hasRated(long note_id, long user_id) {
        try {
            return (from rating in _context.NoteRating
                    where rating.NoteId == note_id && rating.UserId == user_id
                    select rating).Any();
        }
        catch {
            throw;
        }
    }

    public bool getUserRating(long note_id, long user_id) {
        try {
            return (from rating in _context.NoteRating
                    where rating.NoteId == note_id && rating.UserId == user_id
                    select rating.Rate).FirstOrDefault();
        }
        catch {
            throw;
        }
    }

    public void UpdateRating(long note_id, long user_id, bool rating) {
        try {
            NoteRating note_rating = (from nr in _context.NoteRating
                                    where nr.NoteId == note_id && nr.UserId == user_id
                                    select nr).FirstOrDefault() ?? throw new DataNotFoundException("Selected note does not exist");

            note_rating.Rate = rating;
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public void DeleteRating(long note_id, long user_id) {
        try {
            NoteRating note_rating = (from nr in _context.NoteRating
                                    where nr.NoteId == note_id && nr.UserId == user_id
                                    select nr).FirstOrDefault() ?? throw new DataNotFoundException("Selected note does not exist");

            _context.NoteRating.Remove(note_rating);
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }

    public void AddRating(NoteRating rating) {
        try {
            _context.NoteRating.Add(rating);
            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }
}