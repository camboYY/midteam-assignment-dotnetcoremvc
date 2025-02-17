using ExamMidTerm.Models;
using ExamMidTerm.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ExamMidTerm.Controllers
{
    public class PersonController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }


        // GET: Person
        public async Task<IActionResult> Index(int? page = 1)
        {
            return View(await _unitOfWork.Person.GetAll(page, inCludes: "Province,District,Commune,Village"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/person");

                    if (!string.IsNullOrEmpty(person.Image))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, person.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    person.Image = @"/images/person/" + fileName;
                }
                _unitOfWork.Person.Add(person);
                _unitOfWork.save();
                TempData["success"] = "You have successfully created person";
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Create()
        {

            var provinceList = await _unitOfWork.Province.GetAll();
            IEnumerable<SelectListItem> provinces = provinceList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["ProvinceList"] = provinces;

            var DistrictList = await _unitOfWork.District.GetAll();
            IEnumerable<SelectListItem> districts = DistrictList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["DistrictList"] = districts;

            var CommuneList = await _unitOfWork.Commune.GetAll();
            IEnumerable<SelectListItem> communes = CommuneList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["CommuneList"] = communes;

            var VillageList = await _unitOfWork.Village.GetAll();
            IEnumerable<SelectListItem> villages = VillageList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["VillageList"] = villages;

            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _unitOfWork.Person
                .Get(b => b.Id == id, inCludes: "Province,District,Commune,Village");

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var provinceList = await _unitOfWork.Province.GetAll();
            IEnumerable<SelectListItem> provinces = provinceList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["ProvinceList"] = provinces;

            var DistrictList = await _unitOfWork.District.GetAll();
            IEnumerable<SelectListItem> districts = DistrictList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["DistrictList"] = districts;

            var CommuneList = await _unitOfWork.Commune.GetAll();
            IEnumerable<SelectListItem> communes = CommuneList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["CommuneList"] = communes;

            var VillageList = await _unitOfWork.Commune.GetAll();
            IEnumerable<SelectListItem> villages = VillageList.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["VillageList"] = villages;

            var person = await _unitOfWork.Person.Get(b => b.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Person person, IFormFile? file)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images/person");

                    if (!string.IsNullOrEmpty(person.Image))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, person.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    person.Image = @"/images/person/" + fileName;
                }
                _unitOfWork.Person.Update(person);
                _unitOfWork.save();
                TempData["success"] = "You have successfully edited person";
                return RedirectToAction(nameof(Index));

            }
            return View(person);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _unitOfWork.Person.Get
                (m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _unitOfWork.Person.Get(m => m.Id == id);
            if (person != null)
            {
                _unitOfWork.Person.Remove(person);
            }

            _unitOfWork.save();
            TempData["success"] = "You have successfully deleted person";

            return RedirectToAction(nameof(Index));
        }
    }
}
