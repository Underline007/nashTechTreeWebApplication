using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreeWeb.Application.Helper;
using TreeWeb.Application.Interfaces;
using TreeWeb.Application.ViewModels;
using TreeWeb.Core.Entities;

namespace TreeWeb.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            var categories = await _categoryRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    categories = categories.OrderByDescending(c => c.Name).ToList();
                    break;
                default:
                    categories = categories.OrderBy(c => c.Name).ToList();
                    break;
            }

            int pageSize = 3;
            return View(PaginatedList<Category>.Create(categories.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var createCategoryViewModel = new CategoryCreateEditViewModel();
            return View("CreateEdit", createCategoryViewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name
                };
                _categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add category.");
            }

            return View(model);
        }

      
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var editCategoryViewModel = new CategoryCreateEditViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View("CreateEdit", editCategoryViewModel);
        }


      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryCreateEditViewModel model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;

                _categoryRepository.Update(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _categoryRepository.GetByIdAsync(id);
            if (categoryDetails == null) return View("Error");
            return View(categoryDetails);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryDetails = await _categoryRepository.GetByIdAsync(id);
            if (categoryDetails == null) return View("Error");

            _categoryRepository.Delete(categoryDetails);
            return RedirectToAction("Index");
        }
    }
}
