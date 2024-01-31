using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tesst.Dtos;
using Tesst.Models;

namespace Tesst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyMastersController : ControllerBase
    {
        private readonly DummyContext _context;
        private readonly IMapper _mapper;
        public DummyMastersController(DummyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var masters = _context.DummyMasters.Include(x => x.DummyDetails).ToList();

            if (masters == null || masters.Count == 0)
            {
                return NotFound();
            }

            var responses = _mapper.Map<List<MasterReponse>>(masters);
            return Ok(responses.AsQueryable());
        }

        
    }
}
