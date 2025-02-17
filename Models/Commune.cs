using System.ComponentModel.DataAnnotations;

namespace ExamMidTerm.Models;

public class Commune
{
    [Key]
    public int Id { set; get; }
    public string? Code { get; set; }
    public string? Name { get; set; }

}