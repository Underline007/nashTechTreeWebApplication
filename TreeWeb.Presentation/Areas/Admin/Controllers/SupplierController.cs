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
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";
            ViewData["CurrentFilter"] = searchString;

            var suppliers = await _supplierRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s => s.Name.ToLower().Contains(searchString) || s.Address.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Name);
                    break;
                case "Address":
                    suppliers = suppliers.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Address);
                    break;
                default:
                    suppliers = suppliers.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            return View(PaginatedList<Supplier>.Create(suppliers.AsQueryable(), pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> Detail(int id)
        {
            Supplier supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var createSupplierViewModel = new SupplierCreateEditViewModel();
            return View("CreateEdit", createSupplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var supplier = new Supplier
                {
                    Name = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };
                _supplierRepository.Add(supplier);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add supplier.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var editSupplierViewModel = new SupplierCreateEditViewModel
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name,
                Address = supplier.Address,
                PhoneNumber = supplier.PhoneNumber
            };

            return View("CreateEdit", editSupplierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierCreateEditViewModel model)
        {
            if (id != model.SupplierId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var supplier = await _supplierRepository.GetByIdAsync(id);
                if (supplier == null)
                {
                    return NotFound();
                }

                supplier.Name = model.Name;
                supplier.Address = model.Address;
                supplier.PhoneNumber = model.PhoneNumber;

                _supplierRepository.Update(supplier);
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var supplierDetails = await _supplierRepository.GetByIdAsync(id);
            if (supplierDetails == null) return View("Error");
            return View(supplierDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplierDetails = await _supplierRepository.GetByIdAsync(id);
            if (supplierDetails == null) return View("Error");

            _supplierRepository.Delete(supplierDetails);
            return RedirectToAction("Index");
        }



    }
}
