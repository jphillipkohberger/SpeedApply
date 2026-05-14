using Microsoft.AspNetCore.Mvc;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueriesService _queriesService;

        public QueriesController(IQueriesService queriesService)
        {
            _queriesService = queriesService;
        }

        // GET api/<QueriesController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<QueriesDto>> GetQuery(int id)
        {
            var query = await _queriesService.GetQueryByIdAsync(id);
            if (query == null) return NotFound();
            return Ok(query);
        }
    }
}
