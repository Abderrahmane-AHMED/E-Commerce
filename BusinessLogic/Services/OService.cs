using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class OService : IOService
    {
        private readonly IO _oRepository; 
        private readonly ILogger<OService> _logger;

        public OService(IO oRepository, ILogger<OService> logger)
        {
            _oRepository = oRepository; 
            _logger = logger;
        }

        public List<TbO> GetAllO()
        {
                return _oRepository.GetAll(); 
        }

        public List<VwItem> GetAllOData()
        {
                var Os = _oRepository.GetAllData();

                if (Os == null || !Os.Any())
                {
                    _logger.LogWarning("GetAllItemsData() returned NULL or an EMPTY list!");
                    return new List<VwItem>();
                }

                return Os;
        }

        public TbO GetById(int id)
        {
                return _oRepository.GetById(id); 
        }

        public void Save(TbO Os)
        {
                if (Os.OsId == 0)
                {
                    Os.CreatedBy = "1";
                    Os.CreatedDate = DateTime.Now;
                }
                else
                {
                    Os.UpdatedBy = "1";
                    Os.UpdatedDate = DateTime.Now;
                }

                _oRepository.Save(Os); 
        }

        public void DeleteById(int id)
        {
                _oRepository.DeleteById(id); 

        }
    }

}
