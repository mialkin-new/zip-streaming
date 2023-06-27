using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using ZipStreaming.Models;

namespace ZipStreaming.Providers;

public class DocumentsProvider : IDocumentsProvider
{
    private readonly HttpClient _httpClient;

    public DocumentsProvider(HttpClient httpClient) => _httpClient = httpClient;

    public async IAsyncEnumerable<DocumentStream> GetAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var stream = await _httpClient.GetStreamAsync(
            requestUri: "https://raw.githubusercontent.com/StephenClearyExamples/AsyncDynamicZip/master/.gitignore",
            cancellationToken);

        yield return new DocumentStream(FileName: ".gitignore", stream);

        var stream2 = await _httpClient.GetStreamAsync(
            requestUri: "https://raw.githubusercontent.com/StephenClearyExamples/AsyncDynamicZip/master/README.md",
            cancellationToken);

        yield return new DocumentStream(FileName: "readme.md", stream2);
    }
}