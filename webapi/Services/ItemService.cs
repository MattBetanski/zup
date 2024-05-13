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

    public Item GetById(long item_id) {
        return _context.Item.Find(item_id) ?? throw new DataNotFoundException("Select item not found");
    }

    public List<Item> GetWithFilter(long project_id, Models.Type? type, State? state) {
        try {
            List<Item> items = (from itm in _context.Item
                                where itm.ProjectId == project_id && (type == null || itm.Type == type) && (state == null || itm.State == state)
                                select itm).ToList();

            return items;
        }
        catch {
            throw;
        }
    }

    public void Update(Item item) {
        try {
            Item itemToUpdate = (from itm in _context.Item
                         where itm.ItemId == item.ItemId
                         select itm).FirstOrDefault() ?? throw new DataNotFoundException("Selected item not found");
            
            itemToUpdate.Name = item.Name;
            itemToUpdate.Description = item.Description;
            itemToUpdate.State = item.State;
            itemToUpdate.ParentId = item.ParentId;

            _context.SaveChanges();
        }
        catch {
            throw;
        }
    }
}