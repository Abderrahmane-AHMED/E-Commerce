using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class ItemService : IItemService
    {
        private readonly IItem _itemRepository;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItem itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public List<TbItem> GetAllItem()
        {
                return _itemRepository.GetAll().ToList();
        }
        public List<VwItem> GetAllItemsData(int? categoryId)
        {
                var lstCategories = _itemRepository.GetAllItemsData(categoryId);
                return lstCategories;
        }

        public List<VwItem> GetRecommendedItems(int itemId)
        {
                var item = GetById(itemId);
                if (item == null) return new List<VwItem>();

                return _itemRepository.GetRecommendedItems(itemId);
        }

        public List<TbItemImage> GetItemImages(int itemId)
        {
                return _itemRepository.GetItemImages(itemId);  

        }

        public async Task SaveItemImages(List<TbItemImage> images)
        {
            try
            {
                await _itemRepository.SaveItemImages(images); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving item images.");
                throw;
            }
        }

        public TbItem GetById(int id)
        {

                return _itemRepository.GetById(id);
        }
        public VwItem GetItemId(int id)
        {
                return _itemRepository.GetItemId(id);
        }
        public void Save(TbItem item)
        {
                if (item.ItemId == 0)
                {
                    item.CurrentState = 1;
                    item.CreatedBy = "1";
                    item.CreatedDate = DateTime.Now;
                }
                else
                {
                    item.UpdatedBy = "1";
                    item.UpdatedDate = DateTime.Now;
                }

                _itemRepository.Save(item); 
        }
        public void DeleteById(int id)
        {
                _itemRepository.DeleteById(id);

        }
    }
}
