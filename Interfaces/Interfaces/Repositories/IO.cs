using Mediateur.Models;
using System.Collections.Generic;


namespace Mediateur.Interfaces.Repositories
{
    public interface IO
    {
        List<TbO> GetAll();
        List<VwItem> GetAllData();
        TbO GetById(int id);
        void Save(TbO Os);
        void DeleteById(int id);


    }
}
