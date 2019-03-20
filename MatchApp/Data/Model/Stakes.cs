using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    public class Stakes
    {
        [Key]
        public int Id { get; set; }

        public string StakeId { get; set; }

        [StringLength(2)]
        public string StakeKey { get; set; }    //probably enum then convert

   
        public double StakeValue { get; set; }


        public double StakeValueChanged { get; set; }


        public int MatchTimeFk { get; set; }
        [ForeignKey("MatchTimeFk")]
        public MatchTime MatchTime { get; set; }








    }
}
