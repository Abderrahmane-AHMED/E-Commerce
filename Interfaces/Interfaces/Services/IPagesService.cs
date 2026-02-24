using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IPagesService
    {

        List<TbPages> GetAll();
        List<TbPages> GetAllData();
        TbPages GetById(int id);
        void Save(TbPages pages);
        void DeleteById(int id);

    }
}
