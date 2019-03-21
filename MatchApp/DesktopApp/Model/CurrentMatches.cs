using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    public class Stake
    {
        public string StakeID { get; set; }
        public string StakeKey { get; set; }
        public double StakeValue { get; set; }
    }

    public class CurrentMatches
    {
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }
        public DateTime StartTime { get; set; }
        public int CurrentMinutes { get; set; }
        public DateTime FinishTime { get; set; }
        public List<Stake> Stakes { get; set; }
        public bool IsPause { get; set; }
    }
}
