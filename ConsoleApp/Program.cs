using CVFormatter;
using System.IO;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var mainPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            var pathIn = mainPath + "\\resume.pdf";
            var pathOut = mainPath + "\\resume_new.pdf";

            var settings = new FormatterSettings()
            {
                LogoPath = mainPath + "\\nova-logo.jpg",
                BorderPath = mainPath + "\\white-border.jpg"
            };

            var formatter = new ResumeFormatter(settings);

            formatter.Format(pathIn, pathOut);
        }
    }
}