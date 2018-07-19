using System;
using System.IO;
using CVFormatter;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string mainPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            string pathin = mainPath + "\\resume.pdf";
            string pathout = mainPath + "\\resume_new.pdf";

            FormatterSettings Settings = new FormatterSettings();

            Settings.LogoPath = mainPath + "\\nova-logo.jpg";
            Settings.BorderPath = mainPath + "\\white-border.jpg";

            ResumeFormatter Formatter = new ResumeFormatter(Settings);
            Formatter.Format(pathin, pathout);
        }
    }
}
