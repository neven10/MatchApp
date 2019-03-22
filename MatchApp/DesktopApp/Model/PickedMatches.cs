using DesktopApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    public class PickedMatches : BindableBase
    {
        public string EventID { get; set; }
        public string Sport { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Score { get; set; }
        public int CurrentMinutes { get; set; }
        public string StakeKey { get; set; }
        public string StakeValue { get; set; }
      
    }
}
