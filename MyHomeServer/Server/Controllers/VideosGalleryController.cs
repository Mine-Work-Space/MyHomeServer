using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace MyHomeServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosGalleryController : ControllerBase
    {
        [Route("get-pathes")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<string[]> GetVideosPathes()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            return Directory.GetFiles(folderPath, "*.mp4");
        }
    }
}
