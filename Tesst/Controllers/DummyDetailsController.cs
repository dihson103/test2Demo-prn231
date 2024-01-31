using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Tesst.Dtos;
using Tesst.Models;

namespace Tesst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyDetailsController : ControllerBase
    {
        private readonly DummyContext _context;
        private readonly IMapper _mapper;
        public DummyDetailsController(DummyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var details = _context.DummyDetails.Include(x => x.Master).ToList();

            if(details == null )
            {
                return NotFound();
            }

            var response = _mapper.Map<List<DetailResponseWithMaster>>(details);

            return Ok(response.AsQueryable());
        }

        [HttpGet("{masterName}/master-name")]
        public IActionResult GetByMasterName([FromRoute] String masterName)
        {
            var master = _context.DummyMasters.Include(x => x.DummyDetails).SingleOrDefault(x => x.MasterName == masterName);
            if(master == null)
            {
                return NotFound();
            }

            var details = master.DummyDetails;
            if(details == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<DetailsResponse>>(details);
            return Ok();
        }

        [HttpGet("{detailName}/detail-name")]
        public IActionResult GetByDetailName([FromRoute] String detailName)
        {
            var details = _context.DummyDetails.Where(x => x.DetailName== detailName).ToList();

            if(details == null || details.Count() == 0)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<DetailsResponse>>(details);
            return Ok(response);
        }
    }
}
