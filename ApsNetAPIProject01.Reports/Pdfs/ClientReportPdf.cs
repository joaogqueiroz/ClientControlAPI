using ApsNetAPIProject01.Reports.Data;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsNetAPIProject01.Reports.Pdfs
{
    public class ClientReportPdf
    {
        public byte[] ReportGenerator(ClientReportData data) 
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            using (var document = new Document(pdf))
            {
                var img = ImageDataFactory.Create("https://seeklogo.com/images/C/corporate-company-logo-BAE6B43FF7-seeklogo.com.png");
                document.Add(new Image(img));
                document.Add(new Paragraph("\n"));

                document.Add(new Paragraph("Client Report").SetFontSize(24));

                document.Add(new Paragraph("Report generated in " + data.GenerationDate.ToString("MM/dd/yyyy")));
                document.Add(new Paragraph("\n"));

                var table = new Table(3); // 3 ->Number of columns 
                table.AddHeaderCell("Client Name");
                table.AddHeaderCell("Email");
                table.AddHeaderCell("Registration Date");

                foreach (var item in data.Clients)
                {
                    table.AddCell(item.Name);
                    table.AddCell(item.Email);
                    table.AddCell(item.RegistrationDate.ToString("MM/dd/yyyy"));
                }
                //Adding table to pdf
                document.Add(table);

                document.Add(new Paragraph("\n"));

                document.Add(new Paragraph($"Amount of clients {data.Clients.Count}"));
                
            }
            return memoryStream.ToArray();
        }
    }
}
