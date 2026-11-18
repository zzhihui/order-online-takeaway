using Takeaway.Data.Entities.Shops;

namespace Takeaway.Data.Repositories
{
    public interface IMenuSubItemRepository : IDataRepository<MenuSubItem, int>
    {
        MenuSubItem GetSubItemById(int id, int shopId);
    }
}