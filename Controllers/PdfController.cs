using ExamMidTerm.Repositories;
using ExamMidTerm.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamMidTerm.Controllers;
public class PdfController : Controller
{
    private readonly CambodiaNationalIdService _pdfService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PdfController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
        _pdfService = new CambodiaNationalIdService(); // Normally, use dependency injection
    }

    public async Task<IActionResult> DownloadPdf(int? id)
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
        Random random = new Random();
        Guid guid = Guid.NewGuid();
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        string productPath = Path.Combine(wwwRootPath, @"images/person");
        string selfiePath = Path.Combine(wwwRootPath, @"");

        string uniqueId = Guid.NewGuid().ToString("N").Substring(0, 15);

        var pdfBytes = _pdfService.GenerateNationalId(
            person.NameEn,
            person.Name,
            person.DateOfBirth.ToString(),
            person.Gender.ToString(),
            person.Height.ToString(),
            person.Province.Name + " " + person.District.Name + " " + person.Commune.Name + " " + person.Village.Name,
            person.Address,
            person.FromDate + " To" + person.ToDate,
            person.Remark,
            uniqueId,
            selfiePath + person.Image,
            productPath + "/download.png"
        );
        return File(pdfBytes, "application/pdf", "GeneratedDocument.pdf");
    }
}
