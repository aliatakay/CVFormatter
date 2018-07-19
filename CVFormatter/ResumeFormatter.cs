using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CVFormatter
{
    public class ResumeFormatter
        :BaseFormatter
    {
        FormatterSettings formatterSettings;

        public ResumeFormatter(FormatterSettings formatterSettings)
        {
            this.formatterSettings = formatterSettings;
        }

        public void Format(string input, string output)
        {
            var LogoPath = formatterSettings.LogoPath;
            var BorderPath = formatterSettings.BorderPath;
            PdfContentByte pdfContent;

            PdfReader pdfReader = new PdfReader(input);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(output, FileMode.Create));

            int numberOfPages = pdfReader.NumberOfPages;

            for (int i = 0; i < numberOfPages; i++)
            {
                Image logo = Image.GetInstance(LogoPath);
                logo.SetAbsolutePosition(410, 745);
                pdfContent = pdfStamper.GetOverContent(i + 1);
                pdfContent.AddImage(logo);
            }

            Image border = Image.GetInstance(BorderPath);

            border.ScaleAbsoluteHeight(105f);
            border.ScaleAbsoluteWidth(557);
            border.SetAbsolutePosition(26, 515);

            pdfContent = pdfStamper.GetOverContent(1);
            pdfContent.AddImage(border);

            pdfStamper.Close();
        }
    }
}
