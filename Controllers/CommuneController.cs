using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers
{
    public class CommuneController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommuneController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: commune
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.Commune.GetAll(page));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Commune commune)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Commune.Add(commune);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created commune";
                return RedirectToAction(nameof(Index));
            }
            return View(commune);
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

            var commune = await _unitOfWork.Commune
                .Get(b => b.Id == id);

            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commune = await _unitOfWork.Commune.Get(b => b.Id == id);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Commune commune)
        {
            if (id != commune.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Commune.Update(commune);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited commune";
                return RedirectToAction(nameof(Index));

            }
            return View(commune);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commune = await _unitOfWork.Commune.Get
                (m => m.Id == id);
            if (commune == null)
            {
                return NotFound();
            }

            return View(commune);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commune = await _unitOfWork.Commune.Get(m => m.Id == id);
            if (commune != null)
            {
                _unitOfWork.Commune.Remove(commune);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted commune";

            return RedirectToAction(nameof(Index));
        }
    }
}
