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

        // GET api/<FilesController>/GetFileResume/<name>
        [HttpGet("GetFileResume/{name}")]
        public async Task<ActionResult<FilesDto>> GetFileResume(string name)
        {
            var filePath = "/app/Files/" + name;

            Response.Headers.Append("Content-Disposition", "inline; filename=" + name);

            return File(System.IO.File.OpenRead(filePath), "application/pdf");
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
