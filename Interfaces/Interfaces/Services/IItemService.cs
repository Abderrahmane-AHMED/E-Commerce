using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IItemService
    {
        List<TbItem> GetAllItem();
        Task SaveItemImages(List<TbItemImage> images);
        List<TbItemImage> GetItemImages(int itemId);
        List<VwItem> GetAllItemsData(int? categoryId);
        List<VwItem> GetRecommendedItems(int itemId);
        TbItem GetById(int id);
        VwItem GetItemId(int id);
        void Save(TbItem item);
        void DeleteById(int id);
    }
}
