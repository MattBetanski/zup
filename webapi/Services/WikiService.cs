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

    public List<WikiPage> GetByDepartment(long department_id)
    {
        try {
            List<WikiPage> wikis = (from wiki in _context.WikiPage
                                    where wiki.DepartmentId == department_id
                                    select wiki).ToList();
            return wikis;
        }
        catch {
            throw;
        }
    }

    public WikiPage GetWiki(long wiki_id) {
        try {
            WikiPage wiki = (from wp in _context.WikiPage
                            where wp.WikiPageId == wiki_id
                            select wp).FirstOrDefault() ?? throw new DataNotFoundException("Selected wiki does not exist");
            return wiki;
        }
        catch {
            throw;
        }
    }
}