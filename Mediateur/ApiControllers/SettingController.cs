using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace  Mediateur.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        ISetting oSetting;
        public SettingController(ISetting oISetting)
        {
            oSetting = oISetting;
        }
        // GET: api/<SettingController>
        [HttpGet]
        public TbSetting Get()
    {    try
            {
                var oSeeting = oSetting.GetAll();
                return oSeeting;
            }
            catch 
            {
                return new TbSetting();
            }

        }

       

      
    }
}
