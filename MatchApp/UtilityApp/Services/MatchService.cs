using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UtilityApp.Entities;

namespace UtilityApp.Services
{
    public class MatchService
    {
        private static HttpClient httpClient;


        public async Task<Match> GetMatchTimeSpan(TimeSpan interval)
        {
            while (true)
            {
                
                return await GetMatch();
            }
        }
            
             


        private async Task<Match> GetMatch()
        {
            httpClient = new HttpClient();
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://demo.aleacontrol.net/stakesimulator/api/matches"))
            using (var response = await httpClient.SendAsync(request))
            {
                string content = await response.Content.ReadAsStringAsync();         //readasync i dump to string, a ako je json ogroman, onda streamreader
                if(response.IsSuccessStatusCode==false)
                {
                    throw new Exception(((int)response.StatusCode).ToString() + content);  // "log"

                }
                return JsonConvert.DeserializeObject<Match>(content);
            }


        }
    }
}
