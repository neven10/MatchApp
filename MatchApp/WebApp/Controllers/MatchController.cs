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
    public class MatchController : ControllerBase
    {
        private readonly MatchDbContext _context;

        public MatchController(MatchDbContext context)
        {
            _context = context;
        }

        // GET: api/MatchApi
        [HttpGet]
        public MatchViewModel Get()
        {
            return new MatchViewModel
            {
                MatchEvents = _context.MatchEvents.ToList()
            };
            
        }

        // GET: api/MatchApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
