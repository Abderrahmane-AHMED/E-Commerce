using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{
    public class ItemRepository : IItem
    {

        private readonly MediateurContext _context;
        private readonly ILogger<ItemRepository> _logger;
        public ItemRepository(MediateurContext context, ILogger<ItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbItem> GetAll()
        {
           
                return _context.TbItems.ToList();
          
        }
        public List<VwItem> GetAllItemsData(int? categoryId)
        {
           
                return _context.VwItems
                    .Where(a => (categoryId == null || categoryId == 0 || a.CategoryId == categoryId)
                        && a.CurrentState == 1
                        && !string.IsNullOrEmpty(a.ItemName))
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
          
        }

        public List<VwItem> GetRecommendedItems(int itemId)
        {
           
                var item = GetById(itemId);
                if (item == null) return new List<VwItem>();

                return _context.VwItems
                    .Where(a => a.SalesPrice > item.SalesPrice - 150
                        && a.SalesPrice < item.SalesPrice + 150
                        && a.CurrentState == 1)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
           
        }

        public List<TbItemImage> GetItemImages(int itemId)
        {
           
            return _context.TbItemImages.Where(image => image.ItemId == itemId).ToList();
        }
        public async Task SaveItemImages(List<TbItemImage> images)
        {
           
                _context.TbItemImages.AddRange(images); 
                _context.SaveChanges();
         
        }

        public TbItem GetById(int id)
        {
           
                return _context.TbItems.FirstOrDefault(c => c.ItemId == id);
          
        }

        public VwItem GetItemId(int id)
        {
            
                return _context.VwItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
          
        }

        public void Save(TbItem item)
        {
            
                if (item.ItemId == 0)
                {
                    _context.TbItems.Add(item);
                }
                else
                {
                    var existingItem = _context.TbItems.Find(item.ItemId);
                    if (existingItem != null)
                    {
                        _context.Entry(existingItem).CurrentValues.SetValues(item);
                    }
                }
                _context.SaveChanges();
           
        }

        public void DeleteById(int id)
        {
            
                var item = GetById(id);
                if (item != null)
                {
                    _context.TbItems.Remove(item);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogWarning($"Item with ID {id} not found.");
                }
         
        }

    }

}

