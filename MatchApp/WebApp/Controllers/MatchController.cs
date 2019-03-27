using Data;
using MatchApp.Data;
using MatchApp.Data.Model;
using MatchApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly MatchDbContext _context;

        public MatchController(MatchDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<MatchDTO>>> All()
        {
            var match = await _context.MatchEvents.Include(x => x.MatchTimeList).ThenInclude(y => y.StakesList).ToListAsync();         
            
            List <MatchDTO> dto = new List<MatchDTO>();
            foreach(var s in match)
            {
                dto.Add(new MatchDTO
                {
                    Id = s.Id,
                    EventID = s.EventID,
                    Sport = s.Sport,
                    HomeTeam = s.HomeTeam,
                    AwayTeam = s.AwayTeam,
                    CurrentMinutes = s.MatchTimeList.Select(x => x.CurrentMinutes).FirstOrDefault(),
                    Score = s.MatchTimeList.Select(x => x.Score).FirstOrDefault(),
                    StartTime = s.StartTime,
                    FinishTime = s.FinishTime,
                    IsPause = s.MatchTimeList.Select(x => x.IsPause).FirstOrDefault(),
                    StakeValueOne = s.MatchTimeList.Select(x => x.StakesList.Find(u => u.StakeKey == "1")).Select(o => o.StakeValue).FirstOrDefault(),            
                    StakeValueX = s.MatchTimeList.Select(x => x.StakesList.Find(u => u.StakeKey == "X")).Select(o => o.StakeValue).FirstOrDefault(),
                    StakeValueTwo = s.MatchTimeList.Select(x => x.StakesList.Find(u => u.StakeKey == "2")).Select(o => o.StakeValue).FirstOrDefault(),
                    //StakeValueTwo = (from x1 in s.MatchTimeList from x2 in x1.StakesList where x2.StakeKey == "2" select x2.StakeValue).FirstOrDefault(),
                });                 
            }
            return dto; 
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<List<PickedMatchDTO>>> GetPickedMatches()
        {
            var match = await _context.PickedMatches.ToListAsync();
            List<PickedMatchDTO> pickedMatchDTOs = new List<PickedMatchDTO>();
            foreach(var s in match)
            {
                pickedMatchDTOs.Add(new PickedMatchDTO
                {
                    HomeTeam = s.HomeTeam,
                    AwayTeam =s.AwayTeam,
                    Sport =s.Sport,
                    Score=s.Score,
                    Status=s.Status,
                    StakeValueOne =s.StakeValueOne,
                    StakeValueTwo = s.StakeValueTwo,
                    StakeValueX=s.StakeValueX,
                    SelectedStakeValueOne=s.SelectedStakeValueOne,
                    SelectedStakeValueTwo=s.SelectedStakeValueTwo,
                    SelectedStakeValueX=s.SelectedStakeValueX,
                    IsBlocked = s.IsBlocked
                });

            }
            return pickedMatchDTOs;
        }





        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<PickedMatches>>> PostSelectedMatches (List<PickedMatches> matches)
        {
            foreach(var m in matches)
            {            
                _context.PickedMatches.Add(m);
            }           
            await _context.SaveChangesAsync();

            return NoContent(); ;
        }






    }
}
