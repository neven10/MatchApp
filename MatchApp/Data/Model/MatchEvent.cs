using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class MatchEvent
    {
        [Key]
        public int Id { get; set; }
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public List<MatchTime> MatchTimeList { get; set; }

    }


}
