using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mediateur.Data;
using Mediateur.Models;
using Mediateur.Interfaces.Repositories;


namespace Mediateur.Controllers
{
    public class HomeController : Controller
    {
        IItem oClsItems;
      
        ICategories oClsCategories;
        IItemImage oClsItemImages;

        public HomeController(IItem item,  ICategories categories, IItemImage itemImages)
        {
            oClsItems = item;
            
            this.oClsCategories = categories;
            this.oClsItemImages = itemImages;
        }
        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();

            vm.lstAllItems = oClsItems.GetAllItemsData(null).Skip(0).Take(18).ToList();
            vm.lstRecommendedItems = oClsItems.GetAllItemsData(null).Skip(0).Take(10).ToList();
            vm.lstNewItems = oClsItems.GetAllItemsData(null).Skip(0).Take(20).ToList();
            vm.lstFreeDelivry = oClsItems.GetAllItemsData(null).Skip(0).Take(2).ToList();

            foreach (var item in vm.lstAllItems)
                item.ImageName = oClsItemImages.GetByItemId(item.ItemId).FirstOrDefault()?.ImageName;

            foreach (var item in vm.lstRecommendedItems)
                item.ImageName = oClsItemImages.GetByItemId(item.ItemId).FirstOrDefault()?.ImageName;

            foreach (var item in vm.lstNewItems)
                item.ImageName = oClsItemImages.GetByItemId(item.ItemId).FirstOrDefault()?.ImageName;

            foreach (var item in vm.lstFreeDelivry)
                item.ImageName = oClsItemImages.GetByItemId(item.ItemId).FirstOrDefault()?.ImageName;

       
            vm.lstCategories = oClsCategories.GetAll().Take(4).ToList();

            return View(vm);
        }



    }
}
