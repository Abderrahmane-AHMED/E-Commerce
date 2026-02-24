
using Mediateur.Data;
using Mediateur.Models;
using Mediateur.Services;
using Mediateur.Utlities;
using Mediateur.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Mediateur.Areas.admin.Controllers
{
    [Authorize(Roles = "Manager , Admin ")]
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriesService categoriesService, ILogger<CategoriesController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        public IActionResult List()
        {
            try
            {
                List<TbCategory> categories = _categoriesService.GetAllCategories();
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in CategoriesController.List()");
                TempData["ErrorMessage"] = "An error occurred while loading the categories list.";
                return RedirectToAction("ErrorPage");
            }
        }

        public IActionResult Edit(int? categoryId)
        {
            try
            {
                var category = new TbCategory();
                if (categoryId != null)
                {
                    category = _categoriesService.GetById(Convert.ToInt32(categoryId));
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while loading category for editing. ID: {categoryId}");
                TempData["ErrorMessage"] = "An error occurred while loading the category for editing.";
                return RedirectToAction("ErrorPage");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category , List<IFormFile> Files)
        {
              
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Edit", category);
                }
                category.ImageName = await Helper.UploadImage(Files, "Categories");

                _categoriesService.Save(category);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while saving category. ID: {category?.CategoryId}");
                TempData["ErrorMessage"] = "An error occurred while saving the category.";
                return RedirectToAction("ErrorPage");
            }
        }

        async Task<string> UploadImage(List<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads/Categories", ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }
            return string.Empty;
        }

        public IActionResult Delete(int CategoryId)
        {
           
            try
            {
                _categoriesService.DeleteById(Convert.ToInt32(CategoryId));
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting category. ID: {CategoryId}");
                TempData["ErrorMessage"] = "An error occurred while deleting the category.";
                return RedirectToAction("ErrorPage");
            }
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }




}
