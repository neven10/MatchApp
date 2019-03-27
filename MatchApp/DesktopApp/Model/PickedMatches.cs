using DesktopApp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    [JsonObject]
    public class PickedMatches : BindableBase
    {

        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }
        [JsonIgnore]
        public DateTime StartTime { get; set; }
        [JsonIgnore]
        public int CurrentMinutes { get; set; }
        [JsonIgnore]
        public DateTime FinishTime { get; set; }

        public double StakeValueOne { get; set; }
        public double SelectedStakeValueOne { get; set; }

        public double StakeValueX { get; set; }
        public double SelectedStakeValueX { get; set; }

        public double StakeValueTwo { get; set; }
        public double SelectedStakeValueTwo { get; set; }

        [JsonIgnore]
        public bool IsPause { get; set; }
        public string Status { get => GetMatchStatus(); }
        public bool IsBlocked { get; set; }



        private string matchStatus;


        private string GetMatchStatus()
        {
            int timeDiff = ((TimeSpan)(FinishTime - StartTime)).Minutes / 2;

            if (IsPause == true)
            {
                matchStatus = "Paused";
            }
            else if (DateTime.Now == StartTime + TimeSpan.FromMinutes(timeDiff))
            {
                matchStatus = "HalfTime";
            }
            else if (DateTime.Now > FinishTime)
            {
                matchStatus = "Ended";
            }
            else
            {
                matchStatus = CurrentMinutes.ToString() + "minutes";
            }
            return matchStatus;
        }

        private void SetMatchStatus(string value)
        {
            matchStatus = value;
        }






    }
}
