
namespace ExamMidTerm.Services;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

public class CambodiaNationalIdService
{
    public byte[] GenerateNationalId(
        string name,
        string khmerName,
        string dob,
        string sex,
        string height,
        string placeOfBirth,
        string address,
        string duration,
        string remark,
        string idNumber,
        string idImagePath,
        string signaturePath)
    {
        QuestPDF.Settings.EnableDebugging = true;

        QuestPDF.Settings.License = LicenseType.Community;
        using var stream = new MemoryStream();

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5.Landscape());
                page.Margin(20);
                page.Background(Colors.White);

                page.Content().Row(row =>
                {
                    // Left Side: Image & Signature
                    row.RelativeItem(1).Column(col =>
                    {
                        col.Item().Container()
                            .Width(180)  // Set a fixed width
                            .Height(120) // Set a fixed height
                            .Image(idImagePath)
                            .FitWidth(); // Ensures the image fits within the width without overflowing


                        col.Item().Container().AlignCenter()
                            .Width(100)
                            .Height(100)
                            .Image(signaturePath)
                            .FitWidth();
                    });

                    // Right Side: Personal Information
                    row.RelativeItem(2).Column(col =>
                    {

                        col.Item().Container().AlignTop().AlignRight().Text(idNumber)
                            .FontSize(14)
                            .Bold();

                        col.Item().Text($"Name: {name}").FontSize(14).Bold();
                        col.Item().Text(khmerName).FontSize(14).Bold();

                        col.Item().Text($"Date of Birth: {dob}   Sex: {sex}   Height: {height}m")
                            .FontSize(14);

                        col.Item().Text($"Place of Birth: {placeOfBirth}").FontSize(14);
                        col.Item().Text($"Address: {address}").FontSize(14);
                        col.Item().Text($"Duration: {duration}").FontSize(14);
                        col.Item().Text($"Remark: {remark}").FontSize(14);
                    });
                });

                // Footer: MRZ Code
                page.Footer().Text($"ID{idNumber}<<<<<<<<<<<<<<")
                    .FontSize(14)
                    .Bold();
            });
        })
        .GeneratePdf(stream);

        return stream.ToArray();
    }
}
