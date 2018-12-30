using System;
using Newtonsoft.Json;

namespace Open_URL
{
    // 13 video playlist id : PLsRn8zzjiZRgPNekIkiFdbEwzB5JusqOT
    // 113 video playlist id : PLsRn8zzjiZRg81XrxiWeqaQaorEUW5ACf
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking for internet connectivity...");
            if (HelperMethods.CheckForInternetConnection())
            {
                Console.Clear();
                Console.Write("Enter Playlst ID : ");
                string playListID = Console.ReadLine();

                try
                {
                    System.Diagnostics.Process.Start("http://google.com");
                    var result = HelperMethods.GetVideosInPlayListAsync(playListID, "").Result;
                    //Console.Write(result);
                    ResponseActual response = JsonConvert.DeserializeObject<ResponseActual>(result);
                    HelperMethods.OpenVideosinBrowser(response);

                    while (!string.IsNullOrEmpty(response.NextPageToken))
                    {
                        result = HelperMethods.GetVideosInPlayListAsync(playListID, response.NextPageToken).Result;
                        response = JsonConvert.DeserializeObject<ResponseActual>(result);
                        HelperMethods.OpenVideosinBrowser(response);
                    }
                }
                catch (AggregateException ex)
                {
                    foreach (var e in ex.Flatten().InnerExceptions)
                        Console.WriteLine(e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Please connect to internet and press any key to continue.");
                Console.ReadKey();
                Main(null);
            }
            Console.ReadKey();
        }
    }
}
