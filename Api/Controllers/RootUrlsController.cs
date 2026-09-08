using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Interfaces;
using System;
using System.Threading.Tasks;

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

                // 1. Initialize Playwright
                using var playwright = await Playwright.CreateAsync();

                // 2. Set Proxy
                // Make sure proxy server, app server and db server are all on same network speed_network
                var proxyOptions = new Proxy
                {
                    Server = "socks5://speed_apply_haproxy:9050"
                };

                // 3. Launch Browser with custom arguments to reduce automation footprint
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = true,
                    Proxy = proxyOptions,
                    Args = new[] {
                        "--disable-blink-features=AutomationControlled", // Helps hide the webdriver footprint
                        "--disable-infobars",
                        "--no-sandbox"
                    }
                });

                // 4. Create a specialized Context rather than a default page
                // This allows us to inject specific User-Agents and window dimensions
                var context = await browser.NewContextAsync(new BrowserNewContextOptions
                {
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                    ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                    Locale = "en-US",
                    TimezoneId = "America/New_York"
                });

                var page = await context.NewPageAsync();

                // 5. iterate through rootUrls
                foreach (RootUrlsDto rootUrl in rootUrls)
                {
                    //build url
                    string url = "https://" + rootUrl.Domain + rootUrl.SearchPath + query;

                    // Go to the target website
                    await page.GotoAsync(url);

                    // Retrieve the entire HTML source of the page
                    string html = await page.ContentAsync();

                    Console.WriteLine(html);
                }

                await browser.CloseAsync();
                await page.CloseAsync();
                await context.CloseAsync();

            }
            catch (Exception e) 
            { 
                Console.WriteLine(e.ToString());
            }

            return Ok(rootUrls);
        }
    }
}
