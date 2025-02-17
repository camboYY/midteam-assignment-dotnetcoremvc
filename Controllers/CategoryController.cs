using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Category
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.Category.GetAll(page));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created category";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _unitOfWork.Category
                .Get(b => b.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _unitOfWork.Category.Get(b => b.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited category";
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _unitOfWork.Category.Get
                (m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _unitOfWork.Category.Get(m => m.Id == id);
            if (category != null)
            {
                _unitOfWork.Category.Remove(category);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted category";

            return RedirectToAction(nameof(Index));
        }
    }
}
