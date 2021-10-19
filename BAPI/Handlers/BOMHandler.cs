using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BAPI.Handlers.BOMHandler
{
    public class BOM
    {
        public static string retreiveVerse(string verse)
        {
            Root BOM = JsonConvert.DeserializeObject<Root>(File.ReadAllText("book-of-mormon.json"));

            foreach(Book book in BOM.books)
            {
                foreach(Chapter chapt in book.chapters)
                {
                    foreach(Vers v in chapt.verses)
                    {
                        if(v.reference == verse.Trim().ToString())
                        {
                            Console.WriteLine(v.text);
                            return v.reference + " | " + v.text;
                        }
                    }
                }
            }

            return "could not find verse";
        } 
    }

    public class Vers
    {
        public string reference { get; set; }
        public string text { get; set; }
        public int verse { get; set; }
    }

    public class Chapter
    {
        public int chapter { get; set; }
        public string reference { get; set; }
        public List<Vers> verses { get; set; }
        public string heading { get; set; }
    }

    public class Book
    {
        public string book { get; set; }
        public List<Chapter> chapters { get; set; }
        public string full_title { get; set; }
        public string heading { get; set; }
        public string lds_slug { get; set; }
        public string full_subtitle { get; set; }
    }

    public class Testimony
    {
        public string text { get; set; }
        public string title { get; set; }
        public List<string> witnesses { get; set; }
    }

    public class TitlePage
    {
        public string subtitle { get; set; }
        public List<string> text { get; set; }
        public string title { get; set; }
        public string translated_by { get; set; }
    }

    public class Root
    {
        public List<Book> books { get; set; }
        public string last_modified { get; set; }
        public string lds_slug { get; set; }
        public string subtitle { get; set; }
        public List<Testimony> testimonies { get; set; }
        public string title { get; set; }
        public TitlePage title_page { get; set; }
        public int version { get; set; }
    }
}
