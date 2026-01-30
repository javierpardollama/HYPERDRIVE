using Microsoft.SemanticKernel.Text;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Hyperdrive.Ai.Infrastructure.Helpers;

/// <summary>
/// Represents a <see cref="TextHelper"/> class.
/// </summary>
public static class TextHelper
{
    public static string DecodeToUtf8(string @encoded)
    {
        ArgumentNullException.ThrowIfNull(@encoded);

        // Max output bytes for base64: 3 bytes per 4 chars (rounded up)
        int maxBytes = (encoded.Length + 3) / 4 * 3;

        byte[] rented = ArrayPool<byte>.Shared.Rent(maxBytes);

        try
        {
            if (!Convert.TryFromBase64Chars(encoded, rented, out int written))
                throw new FormatException("Invalid Encoded content.");

            return Encoding.UTF8.GetString(rented.AsSpan(0, written));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented, clearArray: true);
        }
    }

    public static List<string> ChunkText(string text)
    {
#pragma warning disable SKEXP0050

        var lines = TextChunker.SplitPlainTextLines(
            text: text,
            maxTokensPerLine: 400,
            tokenCounter: null);

        var chunks = TextChunker.SplitPlainTextParagraphs(
            lines: lines,
            maxTokensPerParagraph: 800,
            tokenCounter: null);

#pragma warning restore SKEXP0050

        return chunks;

    }
}
