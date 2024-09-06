using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace CVFormatter
{
    public class ResumeFormatter : BaseFormatter
    {
        FormatterSettings formatterSettings;

        public ResumeFormatter(FormatterSettings formatterSettings)
        {
            this.formatterSettings = formatterSettings;
        }

        public void Format(string input, string output)
        {
            var logoPath = formatterSettings.LogoPath;
            var borderPath = formatterSettings.BorderPath;

            var pdfReader = new PdfReader(input);
            var pdfContent = default(PdfContentByte);
            var pdfStamper = new PdfStamper(pdfReader, new FileStream(output, FileMode.Create));

            int numberOfPages = pdfReader.NumberOfPages;

            for (int i = 0; i < numberOfPages; i++)
            {
                var logo = Image.GetInstance(logoPath);
                logo.SetAbsolutePosition(410, 745);
                pdfContent = pdfStamper.GetOverContent(i + 1);
                pdfContent.AddImage(logo);
            }

            var border = Image.GetInstance(borderPath);

            border.ScaleAbsoluteHeight(105f);
            border.ScaleAbsoluteWidth(557);
            border.SetAbsolutePosition(26, 515);

            pdfContent = pdfStamper.GetOverContent(1);
            pdfContent.AddImage(border);
            pdfStamper.Close();
        }
    }
}