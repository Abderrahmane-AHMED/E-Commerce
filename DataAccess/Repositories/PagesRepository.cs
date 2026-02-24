using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{
    public class PagesRepository : IPages
    {

        private readonly MediateurContext _context;
        private readonly ILogger<PagesRepository> _logger;
        public PagesRepository(MediateurContext context, ILogger<PagesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbPages> GetAll()
        {

            return _context.TbPages.ToList();

        }
        public List<TbPages> GetAllData()
        {

            if (_context == null)
            {
                _logger.LogError("MediateurContext is NULL!");
                return new List<TbPages>();
            }

            var pages = _context.TbPages.ToList();

            if (pages == null || pages.Any())
            {
                _logger.LogWarning("No data found in VwItems table!");
            }

            return pages ?? new List<TbPages>();

        }


        public TbPages GetById(int id)
        {

            return _context.TbPages.FirstOrDefault(c => c.PageId == id);


        }

        public void Save(TbPages pages)
        {

            if (pages.PageId == 0)
            {
                _context.TbPages.Add(pages);
            }
            else
            {
                _context.TbPages.Update(pages);
            }

            _context.SaveChanges();

        }

        public void DeleteById(int id)
        {

            var pages = GetById(id);
            _context.TbPages.Remove(pages);
            _context.SaveChanges();

        }
    }

    }

