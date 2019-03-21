using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<List<Data.Model.MatchEvent>>>All()
        {

            return await _context.MatchEvents.ToListAsync();
        }


        

       
    }
}
