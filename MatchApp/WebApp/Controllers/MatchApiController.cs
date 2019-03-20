using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchApiController : ControllerBase
    {
        private readonly MatchDbContext _context;

        public MatchApiController(MatchDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public List<Data.Model.MatchEvent> All() => _context.MatchEvents.ToList();


        

       
    }
}
