using DesktopApp.Helpers;
using System;
using System.Collections.Generic;

namespace DesktopApp.Model
{
    public class Stake : BindableBase
    {
        public string StakeID { get; set; }
        public string StakeKey { get; set; }
        public double StakeValue { get; set; }
    }

    public class CurrentMatches : BindableBase
    {
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }
        public DateTime StartTime { get; set; }
        public int CurrentMinutes { get; set; }
        public DateTime FinishTime { get; set; }
        public double StakeValueOne { get; set; }
        public double StakeValueX { get; set; }
        public double StakeValueTwo { get; set; }

        public bool IsPause { get; set; }
        public string Status { get => GetMatchStatus(); }




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
