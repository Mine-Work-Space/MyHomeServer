using Microsoft.AspNetCore.Mvc;
using IO = System.IO;

namespace MyHomeServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("AppendFile/{fragment}")]
        public async Task<bool> UploadFileChunk(int fragment, IFormFile file)
        {
            try
            {
                // ** let the hosted path 
                var filePath = @"D:\Files\" + file.FileName;
                if (fragment == 0 && IO.File.Exists(filePath))
                {
                    IO.File.Delete(filePath);
                    Console.WriteLine("File already exist!");
                }
                using (var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var bw = new BinaryWriter(fileStream))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: {0}", exception.Message);
            }
            return false;
        }
    }

}
