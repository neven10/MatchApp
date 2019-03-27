using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MatchApp.Data.Model
{
    public class PickedMatches
    {
        [Key]
        public int Id { get; set; }
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }
        public string Status { get; set; }

        public double StakeValueOne { get; set; }
        public double SelectedStakeValueOne { get; set; }

        public double StakeValueX { get; set; }
        public double SelectedStakeValueX { get; set; }

        public double StakeValueTwo { get; set; }
        public double SelectedStakeValueTwo { get; set; }

        public bool IsBlocked { get; set; }
    }
}
