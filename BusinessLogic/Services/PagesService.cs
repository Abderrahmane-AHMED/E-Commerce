using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class PagesService : IPagesService
    {
        private readonly IPages _pagesRepository; 
        private readonly ILogger<TbPages> _logger;

        public PagesService(IPages pagesRepository, ILogger<TbPages> logger)
        {
            _pagesRepository = pagesRepository; 
            _logger = logger;
        }

        public List<TbPages> GetAll()
        {
            return _pagesRepository.GetAll();
        }

        public List<TbPages> GetAllData()
        {
                var pages = _pagesRepository.GetAllData();

                if (pages == null || !pages.Any())
                {
                    _logger.LogWarning("GetAllItemsData() returned NULL or an EMPTY list!");
                    return new List<TbPages>();
                }

                return pages;
        }

     

        public TbPages GetById(int id)
        {
                return _pagesRepository.GetById(id);
        }

        public void Save(TbPages pages)
        {
                if (pages.PageId == 0)
                {
                pages.CreatedBy = "1";
           
                }
                else
                {
                pages.UpdatedBy = "1";
              
                }

            _pagesRepository.Save(pages); 
        }

        public void DeleteById(int id)
        {
            _pagesRepository.DeleteById(id); 
        }
    }


}
