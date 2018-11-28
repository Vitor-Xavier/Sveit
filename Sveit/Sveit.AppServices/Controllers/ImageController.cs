using Microsoft.WindowsAzure.Storage;
using Sveit.API.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sveit.AppServices.Controllers
{
    /// <summary>
    /// Gerencia o armazenamento de imagens da aplicação.
    /// </summary>
    public class ImageController : ApiController
    {
        /// <summary>
        /// Tipos de arquivos permitidos.
        /// </summary>
        public List<string> AllowedFileTypes { get; set; } = new List<string> { "image/jpeg", "image/png", "image/bmp" };

        /// <summary>
        /// Arquiva imagem no armazenamento de blob do Azure.
        /// </summary>
        /// <param name="filename">Nome do arquivo</param>
        /// <returns>Endereço absoluto da imagem.</returns>
        [HttpPost]
        [Route("Image/{filename}")]
        public async Task<IHttpActionResult> PostImage(string filename)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var content = await Request.Content.ReadAsMultipartAsync();
            if (!(AllowedFileTypes.Contains(content.Contents[0].Headers.ContentType.MediaType)))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get File extension
            //GetImageFormat(content.Contents[0].Headers.ContentType.MediaType)
            //filename = String.Concat(filename, ".jpeg");

            //// Save file locally
            //Image img = Image.FromStream(await content.Contents[0].ReadAsStreamAsync());
            //string filepath = System.IO.Path.GetTempPath() + "\\" + filename;
            //img.Save(filepath, ImageFormat.Jpeg);
            //return Ok(filepath);

            // Save file to Azure Blob Storage
            filename = String.Concat(filename, GetImageFormat(content.Contents[0].Headers.ContentType.MediaType));
            var storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists();

            var blockBlob = container.GetBlockBlobReference(filename);
            blockBlob.Properties.ContentType = content.Contents[0].Headers.ContentType.ToString();
            using (var fileStream = await content.Contents[0].ReadAsStreamAsync())
            {
                blockBlob.UploadFromStream(fileStream);
            }

            return Ok(blockBlob.Uri.AbsoluteUri);
        }

        private string GetImageFormat(string mediaType)
        {
            switch (mediaType)
            {
                case "image/jpeg": return ".jpg";
                case "image/png": return ".png";
                case "image/bmp": return ".bmp";
                default: return string.Empty;
            }
        }
    }
}
