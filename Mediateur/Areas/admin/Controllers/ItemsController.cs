using Mediateur.Data;
using Mediateur.Models;
using Mediateur.Services;
using Mediateur.Utlities;
using Mediateur.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Mediateur.Areas.admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Mediateur.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mediateur.Areas.admin.Controllers
{
    [Authorize(Roles = "Manager , Admin , Data Entry")]
    [Area("admin")]
    public class ItemsController : Controller
    {

        private readonly IItemService _itemService;
        private readonly IOService _oService;
        private readonly IItemsTypeService _itemsTypeService;
        private readonly IItemImageService _itemImageService;
        private readonly ICategoriesService _categoriesService;

        private readonly ILogger<ItemsController> _logger;
        public ItemsController(IItemService itemService, IOService oService, IItemsTypeService itemsTypeRepository, IItemImageService itemImageService,
            ILogger<ItemsController> logger, ICategoriesService categoriesService)
        {
            _itemService = itemService;
            _itemsTypeService = itemsTypeRepository;
            _itemImageService = itemImageService;
            _categoriesService = categoriesService;
            _oService = oService;
            _logger = logger;
        }

        public IActionResult SomeAction()
        {
            var categories = _categoriesService.GetAllCategories();
            var items = _itemService.GetAllItemsData(null);
            if (categories == null || !categories.Any() || items == null || !items.Any())
            {
                TempData["ErrorMessage"] = "An error occurred while loading data.!";
                return RedirectToAction("ErrorPage");
            }

            ViewData["Categories"] = categories;
            ViewData["Items"] = items;
            return View();
        }


        public IActionResult List()
      {
            try
            {
                _logger.LogInformation("call GetAllItemsData(null)");
                var items = _itemService.GetAllItemsData(null);
              
               
                var itemType = _itemsTypeService.GetAllItemsType() ?? new List<TbItemType>();
                ViewBag.lstItemTypes = itemType;

                if (!items.Any()) 
                {
                    _logger.LogWarning("GetAllItemsData() returned an EMPTY list!");
                    TempData["ErrorMessage"] = "No data to display!";
                }
                else
                {
                    _logger.LogInformation($" GetAllItemsData()  Recover {items.Count} element.");
                }

                ViewBag.lstCategories = _categoriesService.GetAllCategories() ?? new List<TbCategory>();
                ViewBag.lstItemTypes = _itemsTypeService.GetAllItemsType() ?? new List<TbItemType>();
                

                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in ItemsController.List()");
                TempData["ErrorMessage"] = "An error occurred while loading the items list.";
                return RedirectToAction("ErrorPage");

            }
        }

        public IActionResult Search(int categoryId, int? itemTypeId)
        {
            var categories = _categoriesService.GetAllCategories() ?? new List<TbCategory>();
            ViewBag.lstCategories = categories;

            var itemType = _itemsTypeService.GetAllItemsType() ?? new List<TbItemType>();
            ViewBag.lstItemTypes = itemType;

            var items = _itemService.GetAllItemsData(null)?
                .Where(x => x.CategoryId == categoryId && (!itemTypeId.HasValue || x.ItemTypeId == itemTypeId.Value))
                .ToList() ?? new List<VwItem>();

            if (!items.Any())
            {
                TempData["ErrorMessage"] = "No items found for this category.";
            }

            return View("List", items);
        }


        [HttpGet]
        public IActionResult Edit(int? itemId)
        {
            var item = itemId.HasValue ? _itemService.GetById(itemId.Value) ?? new TbItem() : new TbItem();

            try
            {
                ViewBag.lstCategories = _categoriesService.GetAllCategories() ?? new List<TbCategory>();
                ViewBag.lstItemTypes = _itemsTypeService.GetAllItemsType() ?? new List<TbItemType>();
                ViewBag.lstOs = _oService.GetAllO() ?? new List<TbO>();

                return View(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in ItemsController.Edit()");
                TempData["ErrorMessage"] = "An error occurred while loading item data for editing.";
                return RedirectToAction("List");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Edit", item);
                _itemService.Save(item); 
                if (Files != null && Files.Count > 0)
                {
                    item.ImageName = await Helper.UploadImage(Files, "Items");
                    foreach (var file in Files)
                    {
                        if (file.Length > 0)
                        {
                            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Items", imageName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            var image = new TbItemImage
                            {
                                ImageId = 0,
                                ItemId = item.ItemId,
                                ImageName = imageName
                            };
                            _itemImageService.Save(image);
                        }
                    }
                }
                TempData["SuccessMessage"] = "Saved successfully!";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while saving item with ID: {item?.ItemId}");
                TempData["ErrorMessage"] = "An error occurred while saving the item. Please try again.";
                return View("Edit", item);

            }
        }


       
        public IActionResult Delete(int itemId)
        {
            var item = _itemService.GetById(itemId);
           
            try
            {
                if (item == null)
                {
                    TempData["ErrorMessage"] = "Item not found!";
                    return RedirectToAction("List");
                }

                _itemService.DeleteById(itemId);
                TempData["SuccessMessage"] = "The item was successfully deleted..";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting item with ID: {itemId}");
                TempData["ErrorMessage"] = "An error occurred while deleting the item.";
                return RedirectToAction("List");

            }
        }


        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
