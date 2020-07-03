using GradientParser.Services;
using System;
using System.IO;
using static System.String;

namespace GradientParser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! I'm Gradient Parser.");
            Console.WriteLine();
            Console.WriteLine("Please give me link to html file with gradients.");

            var filePath = GetFilePath();

            var html = LoadHtml(filePath);

            Console.WriteLine("Now I need a Tag name, it is the same as an output file name. Without file extension, it's just a name");

            var tag = GetTag();

            var gradients = new HtmlParser().Parse(html, tag);

            File.WriteAllText($".\\{tag}.json", gradients);

            Console.WriteLine("Ok, I'm done!");
            Console.ReadLine();
        }

        private static string GetTag()
        {
            string tag;
                do
            {
                tag = Console.ReadLine();
                if (IsNullOrWhiteSpace(tag))
                    Console.WriteLine("No, it won't work, try again.");
            } while (IsNullOrWhiteSpace(tag));
            
            return tag;
        }

        private static string LoadHtml(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        private static string GetFilePath()
        {
            string filePath;
            do
            {
                filePath = Console.ReadLine();

                if (IsNullOrWhiteSpace(filePath))
                    Console.WriteLine("I'm expecting a path to HTML file. Please give it to me.");
                else if (!filePath.EndsWith("html"))
                    Console.WriteLine("Em, no, it's not an HTML file. Please give me HTML file.");
                else if (!File.Exists(filePath))
                    Console.WriteLine("Sorry, I can't find this file. Please check what you enter and try again.");
            } while (IsNullOrEmpty(filePath));

            return filePath;
        }
    }
}
