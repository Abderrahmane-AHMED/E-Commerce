using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{
    public class CategoriesRepository : ICategories
    {
        private readonly MediateurContext _context;
        private readonly ILogger<CategoriesRepository> _logger;
        public CategoriesRepository(MediateurContext context, ILogger<CategoriesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbCategory> GetAll()
        {
            
                return _context.TbCategories.ToList();
          
        }

        public TbCategory GetById(int id)
        {
            
                return _context.TbCategories.FirstOrDefault(c => c.CategoryId == id);
          

        }

        public void Save(TbCategory category)
        {
           
                if (category.CategoryId == 0)
                {
                    _context.TbCategories.Add(category); 
                }
                else
                {
                    _context.TbCategories.Update(category); 
                }

                _context.SaveChanges();
          
        }

        public void DeleteById(int id)
        {
            
                var Category = GetById(id);
                _context.TbCategories.Remove(Category);
                _context.SaveChanges();
             

           
        }

     
    }
}
