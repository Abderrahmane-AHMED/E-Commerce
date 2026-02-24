using DataAccess.DbContext.Data;
using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;

namespace Mediateur.Repositories
{
    public class SttingRepository  : ISetting
    {

        private readonly MediateurContext _context;
        private readonly ILogger<SttingRepository> _logger;
        public SttingRepository(MediateurContext context, ILogger<SttingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public TbSetting GetAll()
        {
            var lstCategories = _context.TbSettings.FirstOrDefault();
            return lstCategories;
        }

        public void Save(TbSetting setting)
        {
                _context.Update(setting);
                _context.SaveChanges();
        }



    }
}
