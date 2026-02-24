using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{
    public class ItemImageRepository : IItemImage
    {

        private readonly MediateurContext _context;
        private readonly ILogger<ItemImageRepository> _logger;
        public ItemImageRepository(MediateurContext context, ILogger<ItemImageRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

     
        public List<TbItemImage> GetByItemId(int id)
        {
                return _context.TbItemImages.Where(a => a.ItemId == id).ToList();
        }

        public void Save(TbItemImage image)
        {
                _context.TbItemImages.Add(image);
                _context.SaveChanges();
        }






    }
}
