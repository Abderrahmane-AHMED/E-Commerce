using Mediateur.Models;
using System.Collections.Generic;


namespace Mediateur.Interfaces.Repositories
{
    public interface ISetting
    {
        TbSetting GetAll();
        void Save(TbSetting setting);
   
    }
}
