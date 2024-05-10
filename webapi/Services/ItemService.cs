using Microsoft.EntityFrameworkCore;
using webapi.Models;
using webapi.Data;

namespace webapi.Services;

public class ItemService {
    private readonly ZupContext _context;

    public ItemService(ZupContext context) {
        _context = context;
    }

    public Item Create(Item new_item) {
        try {
            _context.Item.Add(new_item);
            _context.SaveChanges();

            return new_item;
        }
        catch {
            throw;
        }
    }

    public Project? ConfirmInProject(long user_id, long project_id) {
        return null;
    }
}