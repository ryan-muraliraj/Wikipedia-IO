using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Cloud.TextToSpeech.V1;
using AIMLbot;

namespace Wikipedia_API_Test
{
    class Program
    {
        public static Bot AI = new Bot();
        public static User myuser = new User("Wikipedia", AI);

        static void Main(string[] args)
        {
            AI.loadSettings();
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = true;

            while (true) { 
            string input = Console.ReadLine();

            Request r = new Request(input, myuser, AI);
            Result res = AI.Chat(r);
            string response = res.Output.TrimEnd('.');
            if(response != "1")
                {
                    Console.WriteLine("\n" + fetchSummary(response) + "\n");
                }
                else
                {
                    Console.WriteLine("\n I am not sure what you are referring to, could you check spelling or try rephrasing your question?");
                }
            //Console.WriteLine(response);
            
            }

        }
        private static string fetchSummary(string title)
        {
            
            WebClient client = new WebClient();
            string data = client.DownloadString("https://en.wikipedia.org/api/rest_v1/page/summary/" + title);
            var ddata = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data);
            return ddata.extract;
            
        }
        
    }
}
