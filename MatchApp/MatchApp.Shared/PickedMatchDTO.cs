namespace MatchApp.Shared
{

    public class PickedMatchDTO
    {

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
