using System;
using System.Collections.Generic;
using System.Text;

namespace MatchApp.Shared
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int CurrentMinutes { get; set; }
        public string Score { get; set; }
        public bool IsPause { get; set; }
        public double StakeValueOne { get; set; }
        public double StakeValueX { get; set; }
        public double StakeValueTwo { get; set; }

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
