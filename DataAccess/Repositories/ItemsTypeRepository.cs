using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{
    public class ItemsTypeRepository : IItemsType
    {

        private readonly MediateurContext _context;
        private readonly ILogger<ItemsTypeRepository> _logger;
        public ItemsTypeRepository(MediateurContext context, ILogger<ItemsTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbItemType> GetAll()
        {
           
                return _context.TbItemTypes.ToList();
          
        }
        public List<VwItem> GetAllData()
        {
            
                if (_context == null)
                {
                    _logger.LogError("MediateurContext is NULL!");
                    return new List<VwItem>();
                }

                var itemsType = _context.VwItems.ToList();

                if (itemsType == null || !itemsType.Any())
                {
                    _logger.LogWarning("No data found in VwItems table!");
                }

                return itemsType ?? new List<VwItem>(); 
          
        }


        public TbItemType GetById(int id)
        {
            
                return _context.TbItemTypes.FirstOrDefault(c => c.ItemTypeId == id);
           

        }

        public void Save(TbItemType itemsType)
        {
            
                if (itemsType.ItemTypeId == 0)
                {
                    _context.TbItemTypes.Add(itemsType); 
                }
                else
                {
                    _context.TbItemTypes.Update(itemsType); 
                }

                _context.SaveChanges();
           
        }

        public void DeleteById(int id)
        {
            
                var itemsType = GetById(id);
                _context.TbItemTypes.Remove(itemsType);
                _context.SaveChanges();

        }

    }
}
