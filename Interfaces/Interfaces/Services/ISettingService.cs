using Mediateur.Models;
using System.Collections.Generic;

namespace Mediateur.Interfaces.Services
{
    public interface ISettingService
    {
        TbSetting GetAll();
        void Save(TbSetting setting);
    }
}
