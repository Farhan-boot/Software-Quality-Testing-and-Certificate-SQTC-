using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels;
using PTSL.GENERIC.Web.Core.Model.EntityViewModels.Project;
using PTSL.GENERIC.Web.Core.Services.Implementation.Project;

namespace PTSL.GENERIC.Web.Core.Controllers.Documents
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> IndexAsync()
        {
          return View();
        }

        public async Task<ActionResult> CreateDocAsync()
        {
            return View();
        }

        //    [HttpPost]
        //    public ActionResult CreateDocumentList()
        //    {
        //        var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);
        //        using (MemoryStream outputStream = new MemoryStream())
        //        {
        //            // Create an iText PdfWriter with the MemoryStream
        //            using (PdfWriter pdfWriter = new PdfWriter(outputStream))
        //            {
        //                // Create an iText PdfDocument with the PdfWriter
        //                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
        //                {
        //                    // Iterate through each HTML content
        //                    foreach (var htmlContent in testScenarios)
        //                    {

        //                        //if (!pdfDocument.IsClosed())
        //                        //{
        //                            // Create a new page in the PDF document
        //                            pdfDocument.AddNewPage();

        //                            // Create an iText ConverterProperties object
        //                            ConverterProperties properties = new ConverterProperties();

        //                            // Convert the HTML content to PDF and add it to the PDF document
        //                            HtmlConverter.ConvertToPdf(htmlContent.Text, pdfDocument, properties);
        //                        //}
        //                    }
        //                }
        //            }

        //            // Set the position to the beginning of the MemoryStream
        //            outputStream.Position = 0;

        //            // Return the combined PDF file as a FileStreamResult, specifying the content type and file name
        //            return File(outputStream, "application/pdf", "combined.pdf");
        //        }

        //        //MemoryStream outputStream = new MemoryStream();
        //        //PdfWriter pdfWriter = new PdfWriter(outputStream);
        //        //PdfDocument pdfDocument = new PdfDocument(pdfWriter);

        //        //foreach (var htmlContent in testScenarios)
        //        //{
        //        //    if (!pdfDocument.IsClosed())
        //        //    {
        //        //        // Create a new page in the PDF document
        //        //        //pdfDocument.AddNewPage();

        //        //        // Create an iText ConverterProperties object
        //        //        ConverterProperties properties = new ConverterProperties();

        //        //        // Convert the HTML content to PDF and add it to the PDF document
        //        //        HtmlConverter.ConvertToPdf(htmlContent.Text, pdfDocument, properties);
        //        //    }
        //        //}

        //        //outputStream.Position = 0;

        //        //// Return the combined PDF file as a FileStreamResult, specifying the content type and file name
        //        //return File(outputStream, "application/pdf", "combined.pdf");
        //    }

        [HttpPost]
        public ActionResult CreateDocumentList()
        {
            var testScenarios = JsonConvert.DeserializeObject<List<DocumentVM>>(HttpContext.Request.Form["Documents"]!);

            byte[] pdfBytes;
            string htmlString = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Signature</title>
    <!-- Bootstrap CSS -->
    <link href=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"" rel=""stylesheet"">
</head>
<body>
    <div class=""container"">
        <div class=""row"">
            <div class=""col"">
                <h4>Project Manager</h4>
                <img src=""https://upload.wikimedia.org/wikipedia/en/d/d4/Samantha_Signature.jpg"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
            </div>
            <div class=""col"">
                <h4>Project Manager</h4>
                <img src=""https://onlinepngtools.com/images/examples-onlinepngtools/marilyn-monroe-signature.png"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
            </div>
            <div class=""col"">
                <h4>Managing Director</h4>
                <img src=""https://upload.wikimedia.org/wikipedia/commons/3/38/Alice_Sara_Ott_-_Signature.jpg"" alt=""Signature"" class=""img-fluid"" width=""200"" height=""100"">
            </div>
        </div>
    </div>

    <!-- Bootstrap JS (Optional) -->
    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js""></script>
</body>
</html>";
            AgreementDocumentsVM agreement = new AgreementDocumentsVM();
            
                agreement.Agreement_FirstPage = testScenarios[0].Text;
                agreement.Agreement_SecondPage = testScenarios[1].Text;
                agreement.Agreement_ThirdPage = testScenarios[2].Text;
                agreement.Agreement_ForthPage = testScenarios[3].Text;
                agreement.Agreement_LastPage = htmlString;

            //DocumentVM signDoc = new DocumentVM();
            //signDoc.Id = "Html_" + (testScenarios.Count + 1).ToString();
            //signDoc.Text = htmlString;
            //testScenarios.Add(signDoc);
            // Create a MemoryStream to hold the combined PDF content
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Create an iText PdfWriter with the MemoryStream
                using (PdfWriter pdfWriter = new PdfWriter(outputStream))
                {
                    // Create an iText PdfDocument with the PdfWriter
                    using (PdfDocument combinedPdfDocument = new PdfDocument(pdfWriter))
                    {
                        foreach (var htmlContent in testScenarios)
                        {
                            // Create a new PdfDocument for each HTML content
                            using (MemoryStream tempStream = new MemoryStream())
                            {
                                using (PdfWriter tempWriter = new PdfWriter(tempStream))
                                {
                                    using (PdfDocument tempPdfDocument = new PdfDocument(tempWriter))
                                    {
                                        // Create a new page in the temporary PDF document
                                        tempPdfDocument.AddNewPage();

                                        // Create an iText ConverterProperties object
                                        ConverterProperties properties = new ConverterProperties();

                                        // Convert the HTML content to PDF and add it to the temporary PDF document
                                        HtmlConverter.ConvertToPdf(htmlContent.Text, tempPdfDocument, properties);
                                    }
                                }

                                // Create a PdfReader from the MemoryStream containing the temporary PDF document content
                                using (MemoryStream tempStreamForReading = new MemoryStream(tempStream.ToArray()))
                                {
                                    using (PdfDocument tempPdfDocument = new PdfDocument(new PdfReader(tempStreamForReading)))
                                    {
                                        // Merge the temporary PDF document into the combined PDF document
                                        tempPdfDocument.CopyPagesTo(1, tempPdfDocument.GetNumberOfPages(), combinedPdfDocument);
                                    }
                                }
                            }
                        }
                    }
                }

                // Get the byte array from the MemoryStream
                pdfBytes = outputStream.ToArray();
            }
           // string filePath = Path.Combine(Path.GetTempPath(), "combined.pdf");
            var testPath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\DocumentFile");
            var filePath =  Path.Combine(testPath, "Test.pdf");
            // Write the byte array to the file
            System.IO.File.WriteAllBytes(filePath, pdfBytes);

            // Return the combined PDF file with the file path
            //return Json(new { filepath = filePath, Filename = "combine.pdf" });
                var localFilePath = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" + "/DocumentFile/Test.Pdf";

             //File(filePath, "application/pdf", "combined.pdf");
            return Ok(localFilePath);
        }


            //// Return the combined PDF file as a FileStreamResult, specifying the content type and file name
            //return File(outputStream, "application/pdf", "combined.pdf");
        


        public ActionResult PdfView()
        {
            return View();
        }
        public ActionResult PdfViewMulti()
        {
            return View();
        }
    }
}
