using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DistrictController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: District
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.District.GetAll(page));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(District district)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.District.Add(district);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created district";
                return RedirectToAction(nameof(Index));
            }
            return View(district);
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

            var district = await _unitOfWork.District
                .Get(b => b.Id == id);

            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _unitOfWork.District.Get(b => b.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, District district)
        {
            if (id != district.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.District.Update(district);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited district";
                return RedirectToAction(nameof(Index));

            }
            return View(district);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var district = await _unitOfWork.District.Get
                (m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var district = await _unitOfWork.District.Get(m => m.Id == id);
            if (district != null)
            {
                _unitOfWork.District.Remove(district);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted district";

            return RedirectToAction(nameof(Index));
        }
    }
}
