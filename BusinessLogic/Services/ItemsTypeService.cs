using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class ItemsTypeService : IItemsTypeService
    {
        private readonly IItemsType _itemsTypeRepository; 
        private readonly ILogger<ItemsTypeService> _logger;

        public ItemsTypeService(IItemsType itemsTypeRepository, ILogger<ItemsTypeService> logger)
        {
            _itemsTypeRepository = itemsTypeRepository; 
            _logger = logger;
        }

        public List<TbItemType> GetAllItemsType()
        { 
                return _itemsTypeRepository.GetAll(); 
        }

        public List<VwItem> GetAllItemsTypeData()
        {
                var items = _itemsTypeRepository.GetAllData();

                if (items == null || !items.Any())
                {
                    _logger.LogWarning("GetAllItemsData() returned NULL or an EMPTY list!");
                    return new List<VwItem>();
                }

                return items;
        }

     

        public TbItemType GetById(int id)
        {
                return _itemsTypeRepository.GetById(id); 
        }

        public void Save(TbItemType itemsType)
        {
                if (itemsType.ItemTypeId == 0)
                {
                    itemsType.CreatedBy = "1";
                    itemsType.CreatedDate = DateTime.Now;
                }
                else
                {
                    itemsType.UpdatedBy = "1";
                    itemsType.UpdatedDate = DateTime.Now;
                }

                _itemsTypeRepository.Save(itemsType); 
        }

        public void DeleteById(int id)
        {
                _itemsTypeRepository.DeleteById(id); 
        }
    }


}
