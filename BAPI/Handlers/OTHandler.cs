using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAPI.Handlers.OTHandler
{
    public class OT
    {
        public static async Task<string> retreiveVerse(string verse)
        {
            Root OT = JsonConvert.DeserializeObject<Root>("old-testament.json");

            foreach(Book books in OT.books)
            {
                foreach(Chapter chapters in books.chapters)
                {
                    foreach(Vers verses in chapters.verses)
                    {
                        if(verses.reference == verse)
                        {
                            return verses.reference + " | " + verses.text;
                        }
                    }
                }
            }

            return null;
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
        public string note { get; set; }
    }

    public class Book
    {
        public string book { get; set; }
        public List<Chapter> chapters { get; set; }
        public string full_title { get; set; }
        public string lds_slug { get; set; }
        public string full_subtitle { get; set; }
    }

    public class Root
    {
        public List<Book> books { get; set; }
        public string last_modified { get; set; }
        public string lds_slug { get; set; }
        public string the_end { get; set; }
        public string title { get; set; }
        public int version { get; set; }
    }


}
