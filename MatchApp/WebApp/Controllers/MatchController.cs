using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.DTO;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
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
                    StakeValueOne = (from x1 in s.MatchTimeList from x2 in x1.StakesList  where x2.StakeKey=="1" select x2.StakeValue).FirstOrDefault(),
                    StakeValueX = (from x1 in s.MatchTimeList from x2 in x1.StakesList where x2.StakeKey == "X" select x2.StakeValue).FirstOrDefault(),
                    StakeValueTwo = (from x1 in s.MatchTimeList from x2 in x1.StakesList where x2.StakeKey == "2" select x2.StakeValue).FirstOrDefault(),
                });                 
            }
            return dto; 
        }


        

       
    }
}
