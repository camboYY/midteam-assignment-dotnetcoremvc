using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers
{
    public class VillageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Village
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.Village.GetAll(page));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Village village)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Village.Add(village);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created village";
                return RedirectToAction(nameof(Index));
            }
            return View(village);
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

            var village = await _unitOfWork.Village
                .Get(b => b.Id == id);

            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _unitOfWork.Village.Get(b => b.Id == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Village village)
        {
            if (id != village.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Village.Update(village);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited village";
                return RedirectToAction(nameof(Index));

            }
            return View(village);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _unitOfWork.Village.Get
                (m => m.Id == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var village = await _unitOfWork.Village.Get(m => m.Id == id);
            if (village != null)
            {
                _unitOfWork.Village.Remove(village);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted village";

            return RedirectToAction(nameof(Index));
        }
    }
}
