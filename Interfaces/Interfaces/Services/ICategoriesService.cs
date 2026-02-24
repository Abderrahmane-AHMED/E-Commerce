using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface ICategoriesService
    {
        List<TbCategory> GetAllCategories();
        TbCategory GetById(int id);
        void Save(TbCategory category);
        void DeleteById(int id);
    }
}
