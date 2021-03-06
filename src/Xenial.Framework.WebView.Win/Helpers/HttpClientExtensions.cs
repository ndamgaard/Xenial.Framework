﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Scissors.Utils.Io;

namespace Xenial.Framework.WebView.Win.Helpers
{
    /// <summary>
    /// Provides HttpClientExtensions for the System.Net.Http.HttpClient
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Downloads the file asynchronous and returns a MemoryStream.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="url">The URL.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="token">The token.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public static async Task<MemoryStream> DownloadFileAsync(
            this HttpClient client,
            string url,
            IProgress<CopyStreamProgressInfo>? progress = null,
            CancellationToken token = default,
            int bufferSize = StreamExtensions.DefaultBufferSize
        ) => (MemoryStream)await client.DownloadFileAsync(url, new MemoryStream(), progress, token, bufferSize);

        /// <summary>
        /// Downloads the file asynchronous and returns the given Stream.
        /// Rewinds the stream if it is seekable.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="url">The URL.</param>
        /// <param name="streamToWrite">The stream to write.</param>
        /// <param name="progress">The progress.</param>
        /// <param name="token">The token.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<Stream> DownloadFileAsync(
            this HttpClient client,
            string url,
            Stream streamToWrite,
            IProgress<CopyStreamProgressInfo>? progress = null,
            CancellationToken token = default,
            int bufferSize = StreamExtensions.DefaultBufferSize
        )
        {
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);

            _ = await response.Content.ReadAsStringAsync(); //Read Headers, Body will be empty

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"The request returned with HTTP status code {response.StatusCode}");
            }

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;

            if (totalBytes != -1L)
            {
                streamToWrite.SetLength(totalBytes);
            }

            using var stream = await response.Content.ReadAsStreamAsync();

            var resultStream = await stream.CopyToAsyncWithProgress(streamToWrite, progress, token, bufferSize, totalBytes);

            if (resultStream.CanSeek)
            {
                resultStream.Position = 0;
            }

            return resultStream;
        }
    }
}
