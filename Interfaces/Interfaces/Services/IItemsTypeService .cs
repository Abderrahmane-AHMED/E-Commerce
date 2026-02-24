using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IItemsTypeService
    {
        List<TbItemType> GetAllItemsType();
        List<VwItem> GetAllItemsTypeData();
        TbItemType GetById(int id);
        void Save(TbItemType itemsType);
        void DeleteById(int id);
    }
}
