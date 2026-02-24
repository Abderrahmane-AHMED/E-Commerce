using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IOService
    {
        List<TbO> GetAllO();
        List<VwItem> GetAllOData();
        TbO GetById(int id);
        void Save(TbO Os);
        void DeleteById(int id);
    }
}
