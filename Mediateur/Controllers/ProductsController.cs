 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mediateur.Data;
using Mediateur.Models;
using Mediateur.Interfaces.Repositories;


namespace Mediateur.Controllers
{
    public class ProductsController : Controller
    {
        IItem oClsItems;
       
        ICategories oClsCategories;
        public ProductsController(IItem item,  ICategories categories)
        {
            oClsItems = item;
            this.oClsCategories = categories;
        }
        [HttpGet]
        public JsonResult GetProducts()
        {
            var items = oClsItems.GetAllItemsData(null)
                                 .Select(item => new {
                                     id = item.ItemId,
                                     title = item.ItemName,
                                     description = item.Description,
                                     price = item.SalesPrice,
                                     category = item.CategoryName, 
                                     image = item.ImageName 
                                 }).ToList();

            return Json(items);
        }

    }
}
