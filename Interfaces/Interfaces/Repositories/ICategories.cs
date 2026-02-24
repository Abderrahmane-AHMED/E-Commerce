using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Repositories
{
    public interface ICategories
    {
        List<TbCategory> GetAll();
        TbCategory GetById(int id);
        void Save(TbCategory category);
        void DeleteById(int id);
    




    }
}
