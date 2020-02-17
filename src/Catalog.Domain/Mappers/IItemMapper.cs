using Catalog.Domain.Entities;
using Catalog.Domain.Requests.Items;

namespace Catalog.Domain.Mappers
{
    public interface IItemMapper
    {
        Item Map(AddItemRequest request);
        Item Map(EditItemRequest request);
        ItemResponse Map(Item item);
    }
}
