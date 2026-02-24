using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using DataAccess.DbContext.Data;

namespace Mediateur.Repositories
{

    public class ORepository : IO
    {




        private readonly MediateurContext _context;
        private readonly ILogger<ORepository> _logger;
        public ORepository(MediateurContext context, ILogger<ORepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TbO> GetAll()
        {
            
                return _context.TbOs.ToList();
          
        }
        public List<VwItem> GetAllData()
        {
            
                if (_context == null)
                {
                    _logger.LogError("MediateurContext is NULL!");
                    return new List<VwItem>();
                }

                var Os = _context.VwItems.ToList();

                if (Os == null || !Os.Any())
                {
                    _logger.LogWarning("No data found in VwItems table!");
                }

                return Os ?? new List<VwItem>(); 
           
        }


        public TbO GetById(int id)
        {
            
                return _context.TbOs.FirstOrDefault(c => c.OsId == id);
           

        }

        public void Save(TbO Os)
        {
            
                if (Os.OsId == 0)
                {
                    _context.TbOs.Add(Os); 
                }
                else
                {
                    _context.TbOs.Update(Os); 
                }

                _context.SaveChanges();
         
        }

        public void DeleteById(int id)
        {
            
                var Os = GetById(id);
                _context.TbOs.Remove(Os);
                _context.SaveChanges();

        }

    }
}

