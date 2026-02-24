using Mediateur.Models;
using System.Collections.Generic;


namespace Mediateur.Interfaces.Repositories
{
    public interface IPages
    {

        List<TbPages> GetAll();
        List<TbPages> GetAllData();
        TbPages GetById(int id);
        void Save(TbPages pages);
        void DeleteById(int id);

    }
}
