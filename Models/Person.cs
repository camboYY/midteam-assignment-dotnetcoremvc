using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExamMidTerm.Models;

public class Person
{
    [Key]
    public int Id { set; get; }
    public double Height { get; set; }
    public Sex Gender { get; set; }
    [DisplayName("Name in Khmer")]
    public string Name { get; set; }
    public string? NameEn { get; set; }

    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    public string? Address { get; set; }
    [DataType(DataType.Date)]

    public DateOnly FromDate { get; set; }
    [DataType(DataType.Date)]
    public DateOnly ToDate { get; set; }
    public string? Remark { get; set; }
    public string? Image { get; set; }

    [DisplayName("Province")]
    public int ProvinceId { set; get; }
    [ValidateNever]
    public Province Province { get; set; }

    [DisplayName("District")]
    public int DistrictId { set; get; }
    [ValidateNever]

    public District District { get; set; }

    [DisplayName("Village")]
    public int VillageId { set; get; }
    [ValidateNever]
    public Village Village { get; set; }
    [DisplayName("Commune")]

    public int CommuneId { set; get; }
    [ValidateNever]

    public Commune Commune { get; set; }

}