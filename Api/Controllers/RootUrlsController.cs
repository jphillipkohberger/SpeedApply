using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Playwright;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;

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
            // Gather rooturls to start querying, linked in etc.
            var rootUrls = await _rootUrlsService.GetRootUrlsAsync(query);
            if (rootUrls == null) return NotFound();


            try {
                // Initialize Playwright
                using var playwright = await Playwright.CreateAsync();
                await using var browser = await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = true
                    });
                var page = await browser.NewPageAsync();

                // iterate through rootUrls
                foreach (RootUrlsDto rootUrl in rootUrls)
                {
                    //build url
                    string url = "https://" + rootUrl.Domain + rootUrl.SearchPath + query;

                    // Go to the target website
                    await page.GotoAsync(url);

                    // Retrieve the entire HTML source of the page
                    string html = await page.ContentAsync();
                }

            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.ToString());
            }

            return Ok(rootUrls);
        }
    }
}
