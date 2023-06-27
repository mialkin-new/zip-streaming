using System.IO.Compression;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;
using ZipStreaming.Providers;

namespace ZipStreaming.Products;

[ApiController]
[Route("api/documents")]
[SwaggerTag("Documents controller description")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentsProvider _documentsProvider;

    public DocumentsController(IDocumentsProvider documentsProvider) => _documentsProvider = documentsProvider;

    [HttpGet("download-zip-archive")]
    [SwaggerOperation("Download ZIP archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DownloadZipArchive(CancellationToken cancellationToken)
    {
        Response.ContentType = MediaTypeNames.Application.Octet;
        Response.Headers.Add(key: HeaderNames.ContentDisposition, value: "attachment; filename=archive.zip");

        using var zipArchive = new ZipArchive(stream: Response.BodyWriter.AsStream(), mode: ZipArchiveMode.Create);

        await foreach (var attachmentStream in _documentsProvider.GetAsync(cancellationToken))
        {
            var zipArchiveEntry = zipArchive.CreateEntry(attachmentStream.FileName);
            await using var entryStream = zipArchiveEntry.Open();
            await attachmentStream.Stream.CopyToAsync(entryStream, cancellationToken);
        }

        return new EmptyResult();
    }
}