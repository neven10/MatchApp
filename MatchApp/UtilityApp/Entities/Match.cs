using System;

namespace UtilityApp.Entities
{
        public class Rootobject
        {
            public Match[] Property1 { get; set; }
        }

        public class Match
    {
            public string EventID { get; set; }
            public string Sport { get; set; }
            public string HomeTeam { get; set; }
            public string AwayTeam { get; set; }
            public string Score { get; set; }
            public DateTime StartTime { get; set; }
            public int CurrentMinutes { get; set; }
            public DateTime FinishTime { get; set; }
            public Stake[] Stakes { get; set; }
            public bool IsPause { get; set; }
        }

        public class Stake
        {
            public string StakeID { get; set; }
            public string StakeKey { get; set; }
            public float StakeValue { get; set; }
        }

    
}
