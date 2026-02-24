using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mediateur.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategories _categoriesRepository;
        private readonly ILogger<CategoriesService> _logger;
        public CategoriesService(ICategories categoriesRepository , ILogger<CategoriesService> logger)
        {
            _categoriesRepository = categoriesRepository;
            _logger = logger;
        }

        public List<TbCategory> GetAllCategories()
        {
           
                return _categoriesRepository.GetAll();
        }
        public TbCategory GetById(int id)
        {
           
                return _categoriesRepository.GetById(id);
          
        }
        public void Save(TbCategory category)
        {
            

                if (category.CategoryId == 0)
                {
                    category.CreatedBy = "1";
                    category.CreatedDate = DateTime.Now;
                }
                else
                {
                    category.UpdatedBy = "1";
                    category.UpdatedDate = DateTime.Now;
                }

                _categoriesRepository.Save(category);
          
        }
        public void DeleteById(int id)
        {
            
                _categoriesRepository.DeleteById(id);

        }




    }
}
