using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Province
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.Province.GetAll(page));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Province province)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Province.Add(province);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created province";
                return RedirectToAction(nameof(Index));
            }
            return View(province);
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

            var province = await _unitOfWork.Province
                .Get(b => b.Id == id);

            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _unitOfWork.Province.Get(b => b.Id == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Province province)
        {
            if (id != province.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Province.Update(province);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited province";
                return RedirectToAction(nameof(Index));

            }
            return View(province);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _unitOfWork.Province.Get
                (m => m.Id == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _unitOfWork.Province.Get(m => m.Id == id);
            if (province != null)
            {
                _unitOfWork.Province.Remove(province);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted province";

            return RedirectToAction(nameof(Index));
        }
    }
}
