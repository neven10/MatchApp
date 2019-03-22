using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
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

    }
}
