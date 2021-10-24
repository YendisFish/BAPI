using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPI.Handlers.BOMHandler;
using BAPI.Handlers.DNCHandler;
using BAPI.Handlers.NTHandler;
using BAPI.Handlers.OTHandler;

namespace BAPI.Handlers.ArrayHandler
{
    public class ArrayHandler
    {
        public string[] VerseArray(string commandtoparse)
        {
            string[] chapter = commandtoparse.Split(':');
            string[] nums = chapter[1].Split("-");

            List<string> verses = new();

            for(int i = Convert.ToInt32(nums[0]); i <= Convert.ToInt32(nums[1]); i++)
            {
                string verse = chapter[0] + ":" + i.ToString();

                Console.WriteLine(verse);

                string toReturn = "";

                try
                {
                    toReturn = BOM.retreiveVerse(verse).ToString();

                    if(toReturn != "could not find verse")
                    {
                        verses.Add(toReturn);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not find in BOM");
                }

                try
                {
                    toReturn = OT.retreiveVerse(verse).ToString();

                    if (toReturn != "could not find verse")
                    {
                        verses.Add(toReturn);
                    }
                }
                catch
                {
                    Console.WriteLine("Couldnt find in OT");
                }

                try
                {
                    toReturn = NT.retreiveVerse(verse).ToString();

                    if (toReturn != "could not find verse")
                    {
                        verses.Add(toReturn);
                    }
                }
                catch
                {
                    Console.WriteLine("Couldnt find in NT");
                }

                try
                {
                    toReturn = DNC.retreiveVerse(verse).ToString();

                    if (toReturn != "could not find verse")
                    {
                        verses.Add(toReturn);
                    }
                }
                catch
                {
                    Console.WriteLine("Couldnt find in DNC");
                }

                Console.WriteLine(toReturn);
            }

            return verses.ToArray();

        }
    }
}
