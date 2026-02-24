using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class ItemImageService : IItemImageService
    {
        private readonly IItemImage _itemImageRepository;
        private readonly ILogger<ItemImageService> _logger;
        public ItemImageService(IItemImage itemImageRepository, ILogger<ItemImageService> logger)
        {
            _itemImageRepository = itemImageRepository;
            _logger = logger;
        }

        public List<TbItemImage> GetByItemId(int id)
        {
           
                var item = _itemImageRepository.GetByItemId(id);
                return item;
        }

        public void Save(TbItemImage image)
        {

                _itemImageRepository.Save(image);
        }


    }
}
