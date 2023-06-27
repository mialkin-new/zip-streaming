using System.Collections.Generic;
using System.Threading;
using ZipStreaming.Models;

namespace ZipStreaming.Providers;

public interface IDocumentsProvider
{
    IAsyncEnumerable<DocumentStream> GetAsync(CancellationToken cancellationToken);
}
