using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BAPI.Handlers.DNCHandler
{
    public class DNC
    {
        public static string retreiveVerse(string verse)
        {
            Root OT = JsonConvert.DeserializeObject<Root>(File.ReadAllText("/app/doctrine-and-covenants.json"));

            foreach(Section sec in OT.sections)
            {
                foreach(Vers v in sec.verses)
                {
                    if(v.reference == verse)
                    {
                        return v.reference + " | " + v.text;
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
    }

    public class Section
    {
        public int section { get; set; }
        public string reference { get; set; }
        public List<Vers> verses { get; set; }
    }

    public class Root
    {
        public string last_modified { get; set; }
        public string lds_slug { get; set; }
        public List<Section> sections { get; set; }
        public string subsubtitle { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public int version { get; set; }
    }


}
