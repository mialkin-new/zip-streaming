using System.IO;

namespace ZipStreaming.Models;

public record DocumentStream(string FileName, Stream Stream);