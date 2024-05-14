using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class WikiService {
    private readonly ZupContext _context;

    public WikiService(ZupContext context) {
        _context = context;
    }

    public WikiPage Create(WikiPage new_wiki) {
        try {
            _context.WikiPage.Add(new_wiki);
            _context.SaveChanges();

            return new_wiki;
        }
        catch {
            throw;
        }
    }
}