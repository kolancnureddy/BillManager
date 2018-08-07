using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BillManager.Entities;

namespace BillManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Dummy")]
    public class DummyController : Controller
    {
        private BillManagerDbContext _context;

        public DummyController(BillManagerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult testBillManager()
        {
            return Ok();
        }
    }
}