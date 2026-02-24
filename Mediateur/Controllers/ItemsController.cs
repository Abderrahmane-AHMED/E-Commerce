using Mediateur.Data;
using Mediateur.Interfaces.Repositories;
using Mediateur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Json;



namespace Mediateur.Controllers
{
    public class ItemsController : Controller
    {


        IItem oItem;
        IItemImage oItemImages;

        private readonly ILogger<ItemsController> _logger;
        public ItemsController(IItem iItem, IItemImage oItemImages, ILogger<ItemsController> logger)
        {
            oItem = iItem;
            this.oItemImages = oItemImages;
            _logger = logger;
        }

        public IActionResult ItemDetails(int id)
        {
            var item = oItem.GetItemId(id);

            if (item == null)
            {
                TempData["ErrorMessage"] = " The Requested Item is not Available.!";
                return RedirectToAction("ItemList"); 
            }

            VmItemDetails vm = new VmItemDetails
            {
                Item = item,
                lstRecommendedItems = oItem.GetRecommendedItems(id)?.Take(12).ToList() ?? new List<VwItem>(),
                lstItemImages = oItemImages.GetByItemId(id) ?? new List<TbItemImage>()
            };

            return View(vm);
        }


        public async Task<IActionResult> ApiItemList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("");

                HttpResponseMessage response = await client.GetAsync("api/Items");

                if (response.IsSuccessStatusCode)
                {
                    var apiData = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    var items = JsonConvert.DeserializeObject<List<TbItem>>(apiData.Data.ToString());
                    return View(items);
                }

                return View(new List<TbItem>()); 
            }
        }


    }
}
