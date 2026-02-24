using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface IItemImageService
    {
        List<TbItemImage> GetByItemId(int id);
        void Save(TbItemImage image);
    }
}
