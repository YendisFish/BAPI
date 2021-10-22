using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BAPI.Handlers.NTHandler
{
    class NT
    {
        public static string retreiveVerse(string verse)
        {
            Root NT = JsonConvert.DeserializeObject<Root>(File.ReadAllText("new-testament.json"));

            foreach(Book book in NT.books)
            {
                foreach(Chapter chapter in book.chapters)
                {
                    foreach(Vers v in chapter.verses)
                    {
                        if(v.reference == verse)
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

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Vers
    {
        public string reference { get; set; }
        public string text { get; set; }
        public int verse { get; set; }
        public bool? pilcrow { get; set; }
    }

    public class Chapter
    {
        public int chapter { get; set; }
        public string reference { get; set; }
        public List<Vers> verses { get; set; }
    }

    public class Book
    {
        public string book { get; set; }
        public List<Chapter> chapters { get; set; }
        public string full_title { get; set; }
        public string lds_slug { get; set; }
        public string note { get; set; }
        public string full_subtitle { get; set; }
    }

    public class TitlePage
    {
        public string subtitle { get; set; }
        public string text { get; set; }
        public string title { get; set; }
    }

    public class Root
    {
        public List<Book> books { get; set; }
        public string last_modified { get; set; }
        public string lds_slug { get; set; }
        public string title { get; set; }
        public TitlePage title_page { get; set; }
        public int version { get; set; }
    }


}
