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
            string[] parserArray = commandtoparse.Split('-');
            int passToForeach = Convert.ToInt32(parserArray[1]);

            List<string> verses = new();

            List<char> fullParsedArray = parserArray[0].ToList();
            int ParsedArrayCount = fullParsedArray.Count();

            for(int i = 0; i < passToForeach; i++)
            {
                int currentNum = fullParsedArray[ParsedArrayCount - i];
                fullParsedArray[ParsedArrayCount] = Convert.ToChar(currentNum.ToString());

                string verse = fullParsedArray.ToString();

                string toReturn = "";

                try
                {
                    toReturn = BOM.retreiveVerse(verse).ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not find in BOM");
                }

                try
                {
                    toReturn = OT.retreiveVerse(verse).ToString();
                }
                catch
                {
                    Console.WriteLine("Couldnt find in OT");
                }

                try
                {
                    toReturn = NT.retreiveVerse(verse).ToString();
                }
                catch
                {
                    Console.WriteLine("Couldnt find in NT");
                }

                try
                {
                    toReturn = DNC.retreiveVerse(verse).ToString();
                }
                catch
                {
                    Console.WriteLine("Couldnt find in DNC");
                }

                verses.Add(toReturn);
            }

            return verses.ToArray();

        }
    }
}
