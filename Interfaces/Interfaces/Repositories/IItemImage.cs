using Mediateur.Models;
using System.Collections.Generic;


namespace Mediateur.Interfaces.Repositories
{
    public interface IItemImage
    {
        List<TbItemImage> GetByItemId(int id);
        void Save(TbItemImage image);
    }
}
