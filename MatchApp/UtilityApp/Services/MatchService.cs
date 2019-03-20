using Data;
using Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UtilityApp.Entities;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace UtilityApp.Services
{
    public class MatchService 
    {
        private static HttpClient httpClient;
        private readonly MatchDbContext _context;
   

        public MatchService(MatchDbContext context)
        {
            _context = context;
        }

       

        public async Task GetMatchTimeSpanAndSave(TimeSpan interval, bool initiate)
        {
  
                while (initiate == true)
                {
                    await Task.Delay(interval);
                    await SaveToDb();
                }

        }

        private async Task SaveToDb()
        {
            List<MatchRoot> matchRoot = await GetMatch();
            List<MatchEvent> matchEvent = new List<MatchEvent>();
            foreach (var e in matchRoot)
            {
                List<Stakes> tempStake = new List<Stakes>();
                e.Stakes.ForEach(x => tempStake.Add(new Stakes { StakeId = x.StakeID, StakeKey = x.StakeKey, StakeValue = x.StakeValue }));

                matchEvent.Add(
                    new MatchEvent
                    {
                        EventID = e.EventID,
                        Sport = e.Sport,
                        HomeTeam = e.HomeTeam,
                        AwayTeam = e.AwayTeam,
                        StartTime = e.StartTime,
                        FinishTime = e.FinishTime,
                        MatchTimeList = new List<MatchTime>
                        {
                            new MatchTime
                            {
                                CurrentMinutes = e.CurrentMinutes,
                                Score = e.Score,
                                IsPause = e.IsPause,
                                StakesList = new List<Stakes>(tempStake)
                                
                            }
                        }
                    });


            }

            _context.MatchEvents.AddRange(matchEvent);
            await _context.SaveChangesAsync();
        }


        private async Task<List<MatchRoot>> GetMatch()
        {

            httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://demo.aleacontrol.net/stakesimulator/api/matches");
            using (var response = await httpClient.SendAsync(request))
            {
                string content = await response.Content.ReadAsStringAsync();         //readasync i dump to string, a ako je json ogroman, onda streamreader
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(((int)response.StatusCode).ToString() + content);  // "log"

                }
                return JsonConvert.DeserializeObject<List<MatchRoot>>(content);
            }


        }

       
    }
}
