using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ZipStreaming.Products;

[ApiController]
[Route("api/documents")]
[SwaggerTag("Documents controller description")]
public class DocumentsController : ControllerBase
{
    [HttpGet("download-zip-archive")]
    [SwaggerOperation("Download ZIP archive")]
    public IActionResult DownloadZipArchive()
    {
        return Ok();
    }
}