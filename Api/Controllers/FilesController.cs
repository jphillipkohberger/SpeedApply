using Microsoft.AspNetCore.Mvc;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;

namespace SpeedApply.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;

        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }

        // GET api/<FilesController>/1
        [HttpGet("{id}")]
        public async Task<ActionResult<FilesDto>> GetFile(int id)
        {
            var file = await _filesService.GetFileByIdAsync(id);
            if (file == null) return NotFound();
            return Ok(file);
        }
    }
}
