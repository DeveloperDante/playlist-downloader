using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Open_URL
{
    public static class HelperMethods
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (WebClient client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static async Task<dynamic> GetVideosInPlayListAsync(string playListID, string nextPageToken)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["key"] = ConfigurationManager.AppSettings["APIKey"],
                ["playlistId"] = playListID,
                ["part"] = "contentDetails",
                ["fields"] = "nextPageToken, items/contentDetails(videoId)",
                ["maxResults"] = "50"
            };
            string baseURL = "https://www.googleapis.com/youtube/v3/playlistItems?";
            string fullURL = MakeURLFromQuery(baseURL, parameters);

            if (!string.IsNullOrEmpty(nextPageToken))
                fullURL += "&pageToken=" + nextPageToken;

            Console.WriteLine("Waiting for response...");

            var result = await new HttpClient().GetStringAsync(fullURL);

            if (result != null)
                return result;
            return default(dynamic);
        }

        private static string MakeURLFromQuery(string baseURL, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (string.IsNullOrEmpty(baseURL))
                throw new ArgumentNullException(nameof(baseURL));

            if (parameters == null || parameters.Count() == 0)
                return baseURL;

            return parameters.Aggregate(baseURL,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }

        public static void OpenVideosinBrowser(ResponseActual response)
        {
            int i = 0;
            for (int j = 0; j < response.Items.Count; j++)
            {
                string videoURL = "https://www.ssyoutube.com/watch?v=" + response.Items[i].ContentDetails.VideoId;
                //Console.WriteLine(videoURL);
                System.Diagnostics.Process.Start(videoURL);
                i++;
                if (i % 5 == 0)
                {
                    Console.WriteLine("Press any key to open next five video.");
                    Console.ReadKey();
                }
            }
        }
    }
}
