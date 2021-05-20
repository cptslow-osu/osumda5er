using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OsuSharp;

namespace osumda5er
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            var client = new OsuClient(
                new OsuSharpConfiguration{
                    ApiKey = "api key here"
                }
            );
            string path="C:/Users/aidanLaptop/Downloads/input.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            List<string> column1 = new List<string>(); //BeatmapID
            List<string> column2 = new List<string>(); //Map Mod
            List<string> column3 = new List<string>(); //Filler for IDs
            foreach (string line in lines) {
                var values = line.Split(",");
                column1.Add(values[0]);
                column2.Add(values[1]);
            }
            int length = column2.Count;
            StreamWriter writer = new StreamWriter("C:/Users/aidanLaptop/Desktop/output.csv");
            for (int i = 0; i < length; i++)
            {
                int tempint = Convert.ToInt32(column1[i]);
                Beatmap temp = await client.GetBeatmapByIdAsync(tempint);
                if (temp != null) 
                {
                    column3.Add(temp.FileMd5);
                }
                else
                {
                    column1[i] = "deleted beatmap";
                    column2[i] = "deleted beatmap";
                    column3.Add("deleted beatmap");
                }
                string outputLine = column1[i] + "," + column2[i] + "," + column3[i];
                writer.WriteLine(outputLine);
            }

            Console.WriteLine(DateTime.Now);    
        }

        
    }
}
