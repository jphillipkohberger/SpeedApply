using Microsoft.AspNetCore.Mvc;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RootUrlsController : ControllerBase
    {
        private readonly IRootUrlsService _rootUrlsService;

        public RootUrlsController(IRootUrlsService rootUrlsService)
        {
            _rootUrlsService = rootUrlsService;
        }

        // GET api/<RootUrlsController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<RootUrlsDto>> GetRootUrl(int id)
        {
            var rootUrl = await _rootUrlsService.GetRootUrlByIdAsync(id);
            if (rootUrl == null) return NotFound();
            return Ok(rootUrl);
        }

        // GET api/<RootUrlsController>/RunQuery
        [HttpGet("RunQuery")]
        public async Task<ActionResult<RootUrlsDto>> RunQuery([FromQuery] string query)
        {
            
            return Ok(new { Query = query });
        }
    }
}
