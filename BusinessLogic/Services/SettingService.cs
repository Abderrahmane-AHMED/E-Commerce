using Mediateur.Interfaces.Repositories;
using Mediateur.Interfaces.Services;
using Mediateur.Models;
using Mediateur.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Mediateur.Services
{

    public class SettingService : ISettingService
    {
        private readonly ISetting _settingRepository; 
        private readonly ILogger<SettingService> _logger;

        public SettingService(ISetting settingRepository, ILogger<SettingService> logger)
        {
            _settingRepository = settingRepository; 
            _logger = logger;
        }

        public TbSetting GetAll()
        { 
                return _settingRepository.GetAll(); 
        }





        public void Save(TbSetting setting)
        {

            _settingRepository.Save(setting);

        }


    }


}
