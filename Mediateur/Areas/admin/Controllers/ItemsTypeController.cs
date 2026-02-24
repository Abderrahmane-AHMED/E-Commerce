using Mediateur.Models;
using Mediateur.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mediateur.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Mediateur.Areas.admin.Controllers
{
    [Authorize(Roles = "Manager , Admin ")]
    [Area("admin")]
    public class ItemsTypeController : Controller
    {
        private readonly IItemsTypeService _itemsTypeService;
        private readonly ILogger<ItemsTypeController> _logger;

        public ItemsTypeController(IItemsTypeService itemsTypeService, ILogger<ItemsTypeController> logger)
        {
            _itemsTypeService = itemsTypeService;
            _logger = logger;
        }

        public IActionResult List()
        {
           

            try
            {
                var itemsType = _itemsTypeService.GetAllItemsType();
                return View(itemsType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in ItemsTypeController.List()");
                TempData["ErrorMessage"] = "An error occurred while loading the list of item types.";
                return RedirectToAction("ErrorPage");
            }
        }

        public IActionResult Edit(int? itemsTypeId)
        {
         
            try
            {
                if (itemsTypeId == null || itemsTypeId == 0)
                {

                    return View(new TbItemType());
                }

                var itemsType = _itemsTypeService.GetById(itemsTypeId.Value);
                if (itemsType == null)
                {
                    TempData["ErrorMessage"] = "The item does not exist or has been deleted. !";
                    return RedirectToAction("List");
                }

                return View(itemsType);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error occurred in ItemsTypeController.Edit()");
                TempData["ErrorMessage"] = "An error occurred while loading the item type for editing.";
                return RedirectToAction("ErrorPage");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItemType itemsType, List<IFormFile> Files)
        {
           
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", itemsType);
                }

                if (Files.Count > 0)
                {
                    itemsType.ImageName = await UploadImage(Files);
                }

                _itemsTypeService.Save(itemsType);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while saving item type with ID: {itemsType}");
                TempData["ErrorMessage"] = "An error occurred while saving the item type.";
                return RedirectToAction("ErrorPage");
            }
        }

        private async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/ItemsType", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return fileName;
                }
            }
            return string.Empty;
        }

        public IActionResult Delete(int itemsTypeId)
        {
           
            try
            {
                _itemsTypeService.DeleteById(itemsTypeId);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, $"Error occurred while deleting item type with ID: {itemsTypeId}");
                TempData["ErrorMessage"] = "An error occurred while deleting the item type.";
                return RedirectToAction("ErrorPage");
            }
        }
    }
}
