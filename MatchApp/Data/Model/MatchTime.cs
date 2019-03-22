using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    public class MatchTime
    {
        [Key]
        public int Id { get; set; }

        public string EventID { get; set; }

        public int CurrentMinutes { get; set; }  //trebalo bi short za db?

        public string Score { get; set; }

        public bool IsPause { get; set; }


        public int EventFk { get; set; }    
        [ForeignKey("EventFk")]
        public MatchEvent MatchEvent { get; set; }

        public List<Stakes> StakesList { get; set; }


    }
}
