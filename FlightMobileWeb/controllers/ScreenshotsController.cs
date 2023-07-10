using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightMobileApp.models;
using System.Net.Http;

namespace FlightMobileApp.controllers
{
    [Route("api/screenshot")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly ScreenshotContext _context;

        public ScreenshotsController(ScreenshotContext context)
        {
            _context = context;
        }

        // GET: api/Screenshots/5
        [HttpGet]
        public async Task<ActionResult<Screenshot>> GetScreenshot()
        {
            using var client = new HttpClient();
            var content = await client.GetByteArrayAsync("http://" + Program.simIP + ":" + Program.screenshotPort + "/screenshot");
            Screenshot img = new Screenshot();
            img.Screenshot_id = Guid.NewGuid().ToString("N").Substring(0, 7);
            img.Img = new byte[content.Length];
            content.CopyTo(img.Img, 0);
            // _context.Screenshots.Add(img);
            //await _context.SaveChangesAsync();
            return Ok(img);
        }
    }
}
