using Mediateur.Models;
using System.Collections.Generic;


namespace Mediateur.Interfaces.Repositories
{
    public interface IItemsType
    {
        List<TbItemType> GetAll();
        List<VwItem> GetAllData();
        TbItemType GetById(int id);
        void Save(TbItemType itemsType);
        void DeleteById(int id);


    }
}
