using Microsoft.AppCenter.Crashes;
using Sveit.Extensions;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IRequestService _requestService;

        public ImageService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<string> PostImage(string filename, string filepath)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ImagesEndpoint);
            builder.AppendToPath(filename);
            string uri = builder.ToString();

            try
            {
                var fileBytes = File.ReadAllBytes(filepath);

                ByteArrayContent byteArray = new ByteArrayContent(fileBytes);
                byteArray.Headers.ContentType = MediaTypeHeaderValue.Parse(GetImageFormat(Path.GetExtension(filepath)));

                return _requestService.PostMimeAsync(uri, new ByteArrayContent[] { byteArray });
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                return Task.FromResult(string.Empty);
            }
        }

        private string GetImageFormat(string extension)
        {
            switch (extension)
            {
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".bmp": return "image/bmp";
                default: return "image/jpeg";
            }
        }
    }
}
